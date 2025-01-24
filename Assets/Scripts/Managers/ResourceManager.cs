using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : Singleton<ResourceManager>
{
    private readonly Dictionary<string, Object> _resourcesPool = new ();

    public T Load<T>(string path) where T : Object
    {
        T res;

        if (_resourcesPool.ContainsKey(path) == false)
        {
            res = Resources.Load<T>(path);
            if (res == null)
            {
                Debug.LogError("ResourceManager Load Fail : " + path);
            }

            _resourcesPool.Add(path, res);
        }

        res = _resourcesPool[path] as T;

        return res;
    }
}