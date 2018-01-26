using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class TableElement
{

    private Dictionary<Type, Object> list = new Dictionary<Type, Object>();


    public void Set<T>(Object obj) where T : class
    {
        if(list.ContainsKey(typeof(T)))
            list[typeof(T)] = obj;
        else
            list.Add(typeof(T), obj);
    }


    public T Get<T>() where T : class
    {
        if(!list.ContainsKey(typeof(T))) {
            var log = string.Format("Object not exist - {0}", typeof(T).Name);
            throw new NullReferenceException(log);
        }

        return list[typeof(T)] as T;
    }

}