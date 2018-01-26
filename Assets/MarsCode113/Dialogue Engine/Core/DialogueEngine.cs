using System;
using System.Collections.Generic;
using UnityEngine;

namespace MarsCode113.DialogueFramework
{
    [SystemTag("Dialogue")]
    public class DialogueEngine : MonoBehaviour, IDialogueEngine
    {

        #region [ Fields / Properties ]

        private static IDialogueEngine instance;
        public static IDialogueEngine Instance {
            get { return instance; }
        }

        [SerializeField]
        private Dictionary<string, IDialogueSystem> systems = new Dictionary<string, IDialogueSystem>();

        [SerializeField]
        private DialogueScript script;

        [SerializeField]
        private DialogueNode node;

        [SerializeField]
        private Vector2 camZone;
        public Vector2 CamZone {
            get { return camZone; }
        }

        [SerializeField]
        private float defaultDuration = 0.5f;
        public float DefaultValue {
            get { return defaultDuration; }
        }

        [SerializeField]
        private float bottomHeight = -2.7f;
        public float BottomHeight {
            get { return bottomHeight; }
        }


        private Action quit;

        #endregion


        #region [ Basic ]

        private void Awake()
        {
            instance = this;

            name = "[Dialogue]";

            DontDestroyOnLoad(gameObject);
        }


        public void RegisterSystem(IDialogueSystem system)
        {
            var systemTag = GetSystemTag(system.GetType());
            if(systems.ContainsKey(systemTag))
                throw new InvalidOperationException(string.Format("System has existed : {0}", systemTag.GetType().Name));

            systems.Add(systemTag, system);
        }


        public void RemoveSystem(IDialogueSystem system)
        {
            var systemTag = GetSystemTag(system.GetType());
            if(!systems.ContainsKey(systemTag))
                throw new NullReferenceException(string.Format("System not exists - {0}", systemTag.GetType().Name));

            systems.Remove(systemTag);
        }


        public T GetSystem<T>() where T : class, IDialogueSystem
        {
            var systemTag = GetSystemTag(typeof(T));
            if(!systems.ContainsKey(systemTag))
                throw new NullReferenceException(string.Format("System not exists : {0}", typeof(T).Name));

            return systems[systemTag] as T;
        }


        private string GetSystemTag(Type type)
        {
            var attributes = type.GetCustomAttributes(typeof(SystemTagAttribute), false) as SystemTagAttribute[];
            return attributes[0].Tag;
        }

        #endregion


        #region [ Core ]

        public void Launch(DialogueScript script)
        {
            this.script = script;
            Read();
        }


        public void SetQuitAction(Action quit)
        {
            this.quit = quit;
        }


        public void Ready(float delay = 0)
        {
            if(delay > 0)
                Invoke("Read", delay);
            else
                Read();
        }


        public void Read()
        {
            node = script.GetFirstNodeAndDequeue();
            if(node == null) {
                Quit();
                return;
            }

            systems[node.Tag].Launch(node);
        }


        public void Quit()
        {
            script = null;
            quit();
        }

        #endregion

    }
}