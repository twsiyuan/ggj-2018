using System;
using Object = UnityEngine.Object;

namespace MarsCode113.ServiceFramework
{
    [SystemTag("Asset")]
    public interface IAssetManager : IServiceManager
    {

        #region [ Asset Bundle ]

        /// <summary>
        /// Return asset from local asset bundle.
        /// </summary>
        T GetAssetBundle<T>(string url, string assetName, PoolUnitCacheTag cacheTag = PoolUnitCacheTag.DontCache) where T : Object;


        /// <summary>
        /// Return asset asynchronously from assetbundle.
        /// </summary>
        void GetAssetBundleAsync(string url, string assetName, PoolUnitCacheTag cacheTag, Action<Object> callback);

        #endregion


        #region [ Resources ]

        T GetResource<T>(string url, PoolUnitCacheTag cacheTag = PoolUnitCacheTag.DontCache) where T : Object;


        void GetResourceAsync<T>(string url, PoolUnitCacheTag cacheTag, Action<Object> callback) where T : Object;

        #endregion


        #region [ Special ]

        /// <summary>
        /// Return asset from preload.
        /// </summary>
        Object GetPreload(string assetName);


        string GetFormattedURL(string index, bool formatWithCatalogue, int symbolID, string replacement);


        string GetFormattedURL(string index);

        #endregion

    }
}