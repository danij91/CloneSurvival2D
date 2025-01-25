using UnityEngine;

public class VfxUnit : PoolingObject
{
    public void OnAnimationEnd()
    {
        Restore();
    }

    internal override void OnInitialize(params object[] parameters)
    {
    }

    protected override void OnUse()
    {
    }

    protected override void OnRestore()
    {
    }
}