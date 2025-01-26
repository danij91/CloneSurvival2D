using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum POOL_TYPE
{
    Sfx,
    Vfx,
    Enemy,
    Projectile,
    Popup
}

public class PoolingManager : Singleton<PoolingManager>
{
    private readonly List<GameObject> _resourceParents = new();
    private readonly Dictionary<POOL_TYPE, List<PoolingObject>> _poolingList = new();
    private const string DEFAULT_PATH = "Prefabs";


    public Transform GetParent(POOL_TYPE type)
    {
        GameObject gameObj = null;
        foreach (var item in _resourceParents)
        {
            if (item.name.Equals(type.ToString()))
            {
                gameObj = item;
            }
        }

        if (gameObj == null)
        {
            gameObj = new GameObject();
            gameObj.name = type.ToString();
            gameObj.transform.SetParent(transform);
            _resourceParents.Add(gameObj);
        }

        return gameObj.transform;
    }

    private T Load<T>(POOL_TYPE type, string resourceName) where T : PoolingObject
    {
        string path = $"{DEFAULT_PATH}/{type}/{resourceName}";

        return ResourceManager.Instance.Load<T>(path);
    }

    private T CreatePoolingObject<T>(POOL_TYPE type, string resourceName)
        where T : PoolingObject
    {
        var obj = Load<T>(type, resourceName);
        if (obj == null)
        {
            Debug.LogError($"{resourceName}이/가 해당 경로에 존재하지 않습니다 : {DEFAULT_PATH}/{type}/{resourceName}");
            return null;
        }

        var newObj = Instantiate(obj, GetParent(type));
        newObj.name = newObj.name.Replace("(Clone)", "");

        if (!_poolingList.ContainsKey(type))
        {
            _poolingList[type] = new List<PoolingObject>();
        }

        _poolingList[type].Add(newObj);

        return newObj;
    }

    public T Create<T>(POOL_TYPE type, string resourceName, Transform parent = null, params object[] parameters)
        where T : PoolingObject
    {
        bool isContainsResources = _poolingList.ContainsKey(type);
        if (isContainsResources)
        {
            var poolingObj = _poolingList[type].Find(x =>
                x.gameObject.name.StartsWith(resourceName)
                && x.PoolingState == POOLING_STATE.WAITING
                && !x.isActiveAndEnabled) as T;

            if (poolingObj != null)
            {
                poolingObj.OnInitialize(parameters);
                poolingObj.Use();
                return poolingObj;
            }
        }

        var newObj = CreatePoolingObject<T>(type, resourceName);
        if (parent != null) newObj.transform.SetParent(parent);

        newObj.OnInitialize(parameters);
        newObj.Use();

        return newObj;
    }

    public T Create<T>(POOL_TYPE type, Vector3 position, string resourceName, Transform parent = null,
        params object[] parameters)
        where T : PoolingObject
    {
        bool isContainsResources = _poolingList.ContainsKey(type);
        if (isContainsResources)
        {
            var poolingObj = _poolingList[type].Find(x =>
                x.gameObject.name.StartsWith(resourceName)
                && x.PoolingState == POOLING_STATE.WAITING
                && !x.isActiveAndEnabled) as T;

            if (poolingObj != null)
            {
                poolingObj.OnInitialize(parameters);
                poolingObj.Use();
                poolingObj.transform.position = position;
                return poolingObj;
            }
        }

        var newObj = CreatePoolingObject<T>(type, resourceName);
        if (parent != null) newObj.transform.SetParent(parent);

        newObj.OnInitialize(parameters);
        newObj.Use();
        newObj.transform.position = position;

        return newObj;
    }
    
    public T CreateUI<T>(POOL_TYPE type, Vector3 position, string resourceName, Transform parent = null,
        params object[] parameters)
        where T : PoolingObject
    {
        bool isContainsResources = _poolingList.ContainsKey(type);
        if (isContainsResources)
        {
            var poolingObj = _poolingList[type].Find(x =>
                x.gameObject.name.StartsWith(resourceName)
                && x.PoolingState == POOLING_STATE.WAITING
                && !x.isActiveAndEnabled) as T;

            if (poolingObj != null)
            {
                poolingObj.OnInitialize(parameters);
                poolingObj.Use();
                poolingObj.GetComponent<RectTransform>().position = position;
                return poolingObj;
            }
        }

        var newObj = CreatePoolingObject<T>(type, resourceName);
        if (parent != null) newObj.transform.SetParent(parent);

        newObj.OnInitialize(parameters);
        newObj.Use();
        newObj.GetComponent<RectTransform>().position = position;

        return newObj;
    }

    public void RestoreAllByType(POOL_TYPE type)
    {
        if (_poolingList.ContainsKey(type))
        {
            foreach (var item in _poolingList[type])
            {
                if (item.PoolingState == POOLING_STATE.WAITING && !item.isActiveAndEnabled)
                    continue;
                if (item != null)
                {
                    item.Restore();
                }
            }
        }
    }

    public void RestoreAll()
    {
        foreach (var pair in _poolingList)
        {
            foreach (var item in pair.Value)
            {
                if (item.PoolingState == POOLING_STATE.WAITING && !item.isActiveAndEnabled)
                    continue;
                if (item != null)
                {
                    item.Restore();
                }
            }
        }
    }
}