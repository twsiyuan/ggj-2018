namespace MarsCode113.ServiceFramework
{
    [SystemTag("Time")]
    public interface ITimeManager : IServiceManager
    {

        void PauseGame();


        void ResumeGame();


        void ScaleTime(float timeScale);

    }
}