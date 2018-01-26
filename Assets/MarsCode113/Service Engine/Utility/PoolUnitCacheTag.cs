namespace MarsCode113.ServiceFramework
{
    public enum PoolUnitCacheTag
    {
        /// <summary>
        /// Unit won't be cached in pool;
        /// </summary>
        DontCache,

        /// <summary>
        /// Unit will be cached in pool until cleaned.
        /// </summary>
        CacheUntilClean,

        /// <summary>
        /// Unit will be cache in pool and never cleaned.
        /// </summary>
        Uncleanable

    } 
}