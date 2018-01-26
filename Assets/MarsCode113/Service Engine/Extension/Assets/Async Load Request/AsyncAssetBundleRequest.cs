using System;
using System.Collections;
using UnityEngine.Networking;
using Object = UnityEngine.Object;

namespace MarsCode113.ServiceFramework
{
    [Serializable]
    public class AsyncAssetBundleRequest : AsnycLoadRequestBase
    {

        private UnityWebRequest request;


        public AsyncAssetBundleRequest(string url, string assetName, PoolUnitCacheTag cacheTag, Action<Object> callback, Action<AsnycLoadRequestBase> reportManager)
        {
            this.url = url;
            this.cacheTag = cacheTag;
            this.reportManager = reportManager;
            this.callback = callback;
            loader = LoadAssetBundleAsync(url, assetName);
        }


        public override float GetRequestProgress()
        {
            return request == null ? 0 : request.downloadProgress;
        }


        private IEnumerator LoadAssetBundleAsync(string url, string assetName)
        {
            request = UnityWebRequest.GetAssetBundle(url);

            yield return request.SendWebRequest();

            if(request.isNetworkError) {
                if(reportManager != null)
                    reportManager(this);
                yield break;
            }

            var assetBundle = DownloadHandlerAssetBundle.GetContent(request);
            asset = assetBundle.LoadAsset(assetName);

            if(reportManager != null)
                reportManager(this);
        }

    }
}