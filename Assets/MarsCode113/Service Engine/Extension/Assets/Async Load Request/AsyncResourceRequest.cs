using System;
using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MarsCode113.ServiceFramework
{
    [Serializable]
    public class AsyncResourceRequest : AsnycLoadRequestBase
    {

        private ResourceRequest request;


        public AsyncResourceRequest(string url, PoolUnitCacheTag cacheTag, Action<Object> callback, Action<AsnycLoadRequestBase> reportManager)
        {
            this.url = url;
            this.cacheTag = cacheTag;
            this.reportManager = reportManager;
            this.callback = callback;
            loader = LoadResourceAsync(url);
        }


        public override float GetRequestProgress()
        {
            return request == null ? 0 : request.progress;
        }


        private IEnumerator LoadResourceAsync(string path)
        {
            request = Resources.LoadAsync(path);

            while(!request.isDone) {
                yield return 0;
            }

            asset = request.asset;

            reportManager(this);
        }

    }
}