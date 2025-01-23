using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    private static readonly object _lock = new object();
    private static bool _isShuttingDown = false;

    public static T Instance
    {
        get
        {
            if (_isShuttingDown)
            {
                Debug.LogWarning($"[Singleton] Instance of {typeof(T)} is already destroyed. Returning null.");
                return null;
            }

            lock (_lock)
            {
                if (_instance == null)
                {
                    // 기존에 존재하는 인스턴스를 찾음
                    _instance = FindObjectOfType<T>();

                    // 씬에 없으면 생성
                    if (_instance == null)
                    {
                        GameObject singletonObject = new GameObject(typeof(T).Name);
                        _instance = singletonObject.AddComponent<T>();

                        // 씬에서 제거되지 않도록 유지
                        DontDestroyOnLoad(singletonObject);
                    }
                }

                return _instance;
            }
        }
    }

    protected virtual void OnDestroy()
    {
        _isShuttingDown = true;
    }

    protected virtual void OnApplicationQuit()
    {
        _isShuttingDown = true;
    }
}