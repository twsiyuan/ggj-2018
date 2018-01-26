using UnityEngine;

namespace MarsCode113.ServiceFramework
{
    public abstract class ServiceManager_Base : MonoBehaviour, IServiceManager
    {

        protected IServiceEngine engine;


        private void OnEnable()
        {
            engine = ServiceEngine.Instance;
            engine.RegisterManager(this);
        }


        private void OnDisable()
        {
            engine.RemoveManager(this);
        }


        public abstract void Clean();

    }
}