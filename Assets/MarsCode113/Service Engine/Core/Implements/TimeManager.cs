using UnityEngine;

namespace MarsCode113.ServiceFramework
{
    public class TimeManager : ServiceManager_Base, ITimeManager
    {

        #region [ Fields / Properties ]

        [SerializeField]
        private float timeScaleFlag = 1;

        [SerializeField]
        private bool onPause;

        #endregion


        public override void Clean()
        {
            timeScaleFlag = 1;

            Time.timeScale = 1;

            onPause = false;
        }


        public void PauseGame()
        {
            if(onPause)
                return;

            onPause = true;

            timeScaleFlag = Time.timeScale;

            Time.timeScale = 0;
        }


        public void ResumeGame()
        {
            onPause = false;

            Time.timeScale = timeScaleFlag;
        }


        public void ScaleTime(float timeScale)
        {
            timeScaleFlag = timeScale;

            if(!onPause)
                Time.timeScale = timeScaleFlag;
        }


        #region [ Editor Compilation ]
#if UNITY_EDITOR
        public float TimeScaleFlag {
            get { return timeScaleFlag; }
        }

        public bool OnPause {
            get { return onPause; }
        }
#endif
        #endregion

    }
}