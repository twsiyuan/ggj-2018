using System;
using UnityEngine;

namespace MarsCode113.ServiceFramework
{

    [Serializable]
    public class PoolUnit<T>
    {

        #region [ Fields / Properties ]

        [SerializeField]
        private T storage;
        public T Storage {
            get { return storage; }
        }

        [SerializeField]
        private PoolUnitCacheTag cacheTag;
        public PoolUnitCacheTag CacheTag {
            get { return cacheTag; }
        }

        #endregion


        public PoolUnit(T storage, PoolUnitCacheTag cacheTag)
        {
            this.storage = storage;
            this.cacheTag = cacheTag;
        }

    }

}