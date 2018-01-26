using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MarsCode113.ServiceFramework
{
    public class AssetManager : ServiceManager_Base, IAssetManager
    {

        #region [ Fields / Properties ]

        [SerializeField]
        private AssetCatalogue catalogue;

        [SerializeField]
        private AssetPack assetPack;

        private Dictionary<string, PoolUnit<Object>> assetPool = new Dictionary<string, PoolUnit<Object>>();

        private Dictionary<string, AsnycLoadRequestBase> asyncLoadRequests = new Dictionary<string, AsnycLoadRequestBase>();

        #endregion


        #region [ Basic ]

        public override void Clean()
        {
            foreach(var storage in assetPool) {
                if(storage.Value.CacheTag != PoolUnitCacheTag.Uncleanable)
                    assetPool.Remove(storage.Key);
            }

            foreach(var request in asyncLoadRequests) {
                StopCoroutine(request.Value.Loader);
                request.Value.Dispose();
                asyncLoadRequests.Remove(request.Key);
            }
        }

        #endregion


        #region [ AssetBundle ]

        public T GetAssetBundle<T>(string url, string assetName, PoolUnitCacheTag cacheTag) where T : Object
        {
            if(assetPool.ContainsKey(url))
                return assetPool[url].Storage as T;

            var assetBundle = AssetBundle.LoadFromFile(url);
            if(assetBundle == null)
                throw new NullReferenceException("Load asset bundle failed : " + url);

            var asset = assetBundle.LoadAsset(assetName);
            AddAssetPoolUnit(url, cacheTag, asset);
            assetBundle.Unload(false);

            return asset as T;
        }


        public void GetAssetBundleAsync(string url, string assetName, PoolUnitCacheTag cacheTag, Action<Object> callback)
        {
            if(assetPool.ContainsKey(url)) {
                callback(assetPool[url].Storage);
            }
            else {
                if(asyncLoadRequests.ContainsKey(url))
                    return;

                var request = new AsyncAssetBundleRequest(url, assetName, cacheTag, callback, ReportLoadRequestResult);
                asyncLoadRequests.Add(url, request);
                StartCoroutine(request.Loader);
            }
        }

        #endregion


        #region [ Resources ]

        public T GetResource<T>(string url, PoolUnitCacheTag cacheTag) where T : Object
        {
            if(assetPool.ContainsKey(url))
                return assetPool[url].Storage as T;

            var asset = Resources.Load<T>(url);
            AddAssetPoolUnit(url, cacheTag, asset);

            return asset as T;
        }


        public void GetResourceAsync<T>(string url, PoolUnitCacheTag cacheTag, Action<Object> callback) where T : Object
        {
            //var request = Resources.LoadAsync<T>(url);
        }

        #endregion


        #region [ Special ]

        public Object GetPreload(string index)
        {
            if(assetPack == null)
                return null;

            return assetPack.GetAsset(index);
        }


        public string GetFormattedURL(string index, bool formatWithCatalogue, int symbolID, string replacement)
        {
            var url = GetFormattedURL(index);
            return FormatURL(url, symbolID, replacement);
        }


        public string GetFormattedURL(string index)
        {
            return catalogue.GetAssetBundleURL(index);
        }

        #endregion


        #region [ Utility ]

        private string FormatURL(string input, int symbolID, string replacement)
        {
            if(input.Contains("#DataPath#"))
                input = input.Replace("#DataPath#", Application.dataPath);

            else if(input.Contains("#Persistent#"))
                input = input.Replace("#Persistent#", Application.persistentDataPath);

            else if(input.Contains("#Streaming#"))
                input = input.Replace("#Streaming#", Application.streamingAssetsPath);

            if(symbolID >= 0) {
                var regex = new Regex(catalogue.Symbol[symbolID]);
                input = regex.Replace(input, replacement);
            }

            return input;
        }

        /// <summary>
        /// Add unit into asset pool.
        /// </summary>
        private void AddAssetPoolUnit(string url, PoolUnitCacheTag cacheTag, Object asset)
        {
            if(cacheTag == PoolUnitCacheTag.DontCache)
                return;

            if(url == "")
                throw new InvalidOperationException("AddAssetPoolUnit - url is empty");

            if(asset == null)
                throw new NullReferenceException("AddAssetPoolUnit - asset is null");

            assetPool.Add(url, new PoolUnit<Object>(asset, cacheTag));
        }


        /// <summary>
        /// Called by AsnycLoadRequestBase implement classes.
        /// </summary>
        private void ReportLoadRequestResult(AsnycLoadRequestBase request)
        {
            if(request.Asset == null)
                Debug.LogError("AsyncL load request failed:\n" + request.Url);
            else
                AddAssetPoolUnit(request.Url, request.CacheTag, request.Asset);

            if(request.Callback != null)
                request.Callback(request.Asset);

            asyncLoadRequests.Remove(request.Url);
            request.Dispose();
        }

        #endregion


        #region [ Editor Compilation ]
#if UNITY_EDITOR
        public Dictionary<string, PoolUnit<Object>> AssetPool {
            get { return assetPool; }
        }

        public Dictionary<string, AsnycLoadRequestBase> AsyncLoadRequests {
            get { return asyncLoadRequests; }
        }
#endif
        #endregion

    }
}