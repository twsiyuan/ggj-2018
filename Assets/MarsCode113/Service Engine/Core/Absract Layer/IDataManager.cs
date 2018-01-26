namespace MarsCode113.ServiceFramework
{
    [SystemTag("Data")]
    public interface IDataManager : IServiceManager
    {

        void SetData(string index, object data, PoolUnitCacheTag cacheTag = PoolUnitCacheTag.CacheUntilClean);


        T GetData<T>(string index);


        void SaveFile(object data, string path);


        T LoadFile<T>(string path);

    }
}