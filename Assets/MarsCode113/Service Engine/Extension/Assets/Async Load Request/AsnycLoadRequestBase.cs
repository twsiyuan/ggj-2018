using System;
using System.Collections;
using Object = UnityEngine.Object;

namespace MarsCode113.ServiceFramework
{
    [Serializable]
    public abstract class AsnycLoadRequestBase
    {

        #region [ Fields / Properties ]
        protected Object asset;
        public Object Asset
        {
            get { return asset; }
        }

        protected string url;
        public string Url
        {
            get { return url; }
        }

        protected PoolUnitCacheTag cacheTag;
        public PoolUnitCacheTag CacheTag
        {
            get { return cacheTag; }
        }

        protected IEnumerator loader;
        public IEnumerator Loader
        {
            get { return loader; }
        }

        protected Action<Object> callback;
        public Action<Object> Callback
        {
            get { return callback; }
        }

        protected Action<AsnycLoadRequestBase> reportManager;
        #endregion


        #region [ Basic ]

        public void Dispose()
        {
            asset = null;
            reportManager = null;
            callback = null;
            loader = null;
        }


        public abstract float GetRequestProgress();

        #endregion


        #region [ Editor Compilation ]
#if UNITY_EDITOR
        public void SetURL(string url)
        {
            this.url = url;
        }
#endif
        #endregion

    }
}