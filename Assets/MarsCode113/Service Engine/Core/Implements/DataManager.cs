using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System;

namespace MarsCode113.ServiceFramework
{
    public class DataManager : ServiceManager_Base, IDataManager
    {

        private Dictionary<string, PoolUnit<object>> dataPool = new Dictionary<string, PoolUnit<object>>();


        public override void Clean()
        {
            foreach(var data in dataPool) {
                if(data.Value.CacheTag != PoolUnitCacheTag.Uncleanable)
                    dataPool.Remove(data.Key);
            }
        }


        public void SetData(string index, object data, PoolUnitCacheTag cacheTag = PoolUnitCacheTag.CacheUntilClean)
        {
            if(dataPool.ContainsKey(index))
                dataPool[index] = new PoolUnit<object>(data, cacheTag);
            else
                dataPool.Add(index, new PoolUnit<object>(data, cacheTag));
        }


        public T GetData<T>(string index)
        {
            if(!dataPool.ContainsKey(index))
                throw new NullReferenceException(string.Format("Data dose not exist : {0}", index));

            return (T)dataPool[index].Storage;
        }


        public void SaveFile(object data, string path)
        {
            using(var fs = new FileStream(path, FileMode.Create)) {
                var bf = new BinaryFormatter();
                var json = JsonUtility.ToJson(data);
                bf.Serialize(fs, json);
            }
        }


        public T LoadFile<T>(string path)
        {
            using(var fs = new FileStream(path, FileMode.Open)) {
                var bf = new BinaryFormatter();
                var json = bf.Deserialize(fs).ToString();
                return JsonUtility.FromJson<T>(json);
            }
        }


        #region [ Editor Compilation ]
#if UNITY_EDITOR
        public Dictionary<string, PoolUnit<object>> DataPool {
            get { return dataPool; }
        }
#endif
        #endregion

    }
}