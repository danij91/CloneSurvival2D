using UnityEngine;

public enum POOLING_STATE
{
    WAITING,
    USING,
}

public abstract class PoolingObject : MonoBehaviour
{
    private POOLING_STATE _poolingState = POOLING_STATE.WAITING;
    private Transform _cachedTransform;

    public new Transform transform
    {
        get
        {
            if (null == _cachedTransform)
            {
                _cachedTransform = GetComponent<Transform>();
            }

            return _cachedTransform;
        }
    }

    public POOLING_STATE PoolingState
    {
        get { return _poolingState; }
        private set
        {
            _poolingState = value;
            switch (_poolingState)
            {
                case POOLING_STATE.USING:
                    gameObject.SetActive(true);
                    OnUse();
                    break;
                case POOLING_STATE.WAITING:
                    OnRestore();
                    gameObject.SetActive(false);
                    break;
            }
        }
    }

    public void Use()
    {
        PoolingState = POOLING_STATE.USING;
    }

    public void Restore()
    {
        if (PoolingState == POOLING_STATE.WAITING)
        {
            return;
        }

        PoolingState = POOLING_STATE.WAITING;
    }

    internal abstract void OnInitialize(params object[] parameters);
    protected abstract void OnUse();
    protected abstract void OnRestore();
}