namespace MarsCode113.ServiceFramework
{
    [SystemTag("Framework")]
    public interface IServiceEngine
    {

        ISceneManager Scene { get; set; }


        void RegisterManager(IServiceManager manager);


        void RemoveManager(IServiceManager manager);


        T GetManager<T>() where T : class, IServiceManager;


        void SwitchScene(string index);


        void ReloadScene();


        void QuitGame();

    }
}