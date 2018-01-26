using System;
using System.Collections.Generic;
using UnityEngine;

namespace MarsCode113.ServiceFramework
{
    public class ServiceEngine : MonoBehaviour, IServiceEngine
    {

        #region [ Fields / Properties ]

        private static IServiceEngine instance;
        public static IServiceEngine Instance {
            get { return instance; }
        }

        private ISceneManager scene;
        public ISceneManager Scene {
            get { return scene; }
            set { scene = value; }
        }

        private Dictionary<string, IServiceManager> managers = new Dictionary<string, IServiceManager>();

        #endregion


        #region [ Initial ]

        private void Awake()
        {
            instance = this;

            name = "[Service Engine]";
            transform.Find("Audio Player").hideFlags = HideFlags.HideInHierarchy;

            DontDestroyOnLoad(gameObject);
        }

        #endregion


        #region [ Service Managers ]

        public void RegisterManager(IServiceManager manager)
        {
            var serviceTag = GetSystemTag(manager.GetType());

            if(managers.ContainsKey(serviceTag))
                throw new InvalidOperationException(string.Format("Manager has existed : {0}", serviceTag));

            managers.Add(serviceTag, manager);
        }


        public void RemoveManager(IServiceManager manager)
        {
            var serviceTag = GetSystemTag(manager.GetType());

            if(!managers.ContainsKey(serviceTag))
                throw new NullReferenceException(string.Format("Manager dose not exist : {0}", serviceTag));

            managers.Remove(serviceTag);
        }


        public T GetManager<T>() where T : class, IServiceManager
        {
            var serviceTag = GetSystemTag(typeof(T));

            if(!managers.ContainsKey(serviceTag))
                throw new NullReferenceException(string.Format("Manager dose not exist : {0}", serviceTag));

            return managers[serviceTag] as T;
        }


        private string GetSystemTag(Type type)
        {
            SystemTagAttribute[] attributes;

            if(type.IsInterface)
                attributes = type.GetCustomAttributes(typeof(SystemTagAttribute), false) as SystemTagAttribute[];
            else
                attributes = type.GetInterfaces()[0].GetCustomAttributes(typeof(SystemTagAttribute), false) as SystemTagAttribute[];

            return attributes[0].Tag;
        }

        #endregion


        #region [ Scene Flow ]

        public void SwitchScene(string index)
        {
            foreach(var framework in managers)
                framework.Value.Clean();

            GetManager<ITimeManager>().ScaleTime(1);
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(index, UnityEngine.SceneManagement.LoadSceneMode.Single);
        }


        public void ReloadScene()
        {
            foreach(var framework in managers)
                framework.Value.Clean();

            GetManager<ITimeManager>().ScaleTime(1);
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        }


        public void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        	Application.Quit();
#endif
        }

        #endregion


        #region [ Editor Compilation ]
#if UNITY_EDITOR
        public Dictionary<string, IServiceManager> Managers {
            get { return managers; }
        }
#endif
        #endregion

    }
}