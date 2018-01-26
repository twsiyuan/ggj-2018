using UnityEngine;

namespace MarsCode113.DialogueFramework
{
    public abstract class ScreenFX_Base : MonoBehaviour, IScreenFX
    {

        [SerializeField]
        protected Vector3 localPos;


        private void Awake()
        {
            transform.localPosition = localPos;
        }


        public void Set(string cmd)
        {
            if(cmd == "0")
                Stop();
            else
                Play();
        }


        public virtual void Play() { }


        public virtual void Stop() { }

    }
}