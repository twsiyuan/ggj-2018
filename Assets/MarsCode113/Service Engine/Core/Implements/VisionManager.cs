using System.Collections.Generic;
using ListExtension.ElementControl;
using UnityEngine;

namespace MarsCode113.ServiceFramework
{
    public class VisionManager : ServiceManager_Base, IVisionManager
    {

        #region [ Fields / Properties ]

        private List<ISubpage> subpages = new List<ISubpage>();

        [SerializeField]
        private bool autoPauseWithSubpage;

        #endregion


        #region [ Basic ]

        public override void Clean()
        {
            subpages = new List<ISubpage>();
        }

        #endregion


        #region [ Subpage ]

        public void OpenPage(ISubpage subpage)
        {
            if(subpages.Count == 0) {
                if(autoPauseWithSubpage)
                    ServiceEngine.Instance.GetManager<ITimeManager>().PauseGame();
            }
            else
                subpages.PeekLast().OnFocus(false);

            subpages.Add(subpage);
            subpage.Open();
        }


        public void SwitchPage(ISubpage subpage)
        {
            subpages.Pop().Close();
            subpages.Add(subpage);
        }


        public void ClosePage()
        {
            subpages.Pop().Close();

            if(subpages.Count == 0) {
                ServiceEngine.Instance.Scene.OnFocus(true);

                if(autoPauseWithSubpage)
                    ServiceEngine.Instance.GetManager<ITimeManager>().ResumeGame();
            }
        }

        #endregion


        #region [ Editor Compilation ]
#if UNITY_EDITOR
        public List<ISubpage> Subpages {
            get { return subpages; }
        }
#endif
        #endregion

    }
}