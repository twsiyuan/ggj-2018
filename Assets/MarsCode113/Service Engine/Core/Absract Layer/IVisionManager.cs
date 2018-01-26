namespace MarsCode113.ServiceFramework
{
    [SystemTag("Vision")]
    public interface IVisionManager : IServiceManager
    {

        void OpenPage(ISubpage subpage);


        void SwitchPage(ISubpage subpage);


        void ClosePage();

    }
}