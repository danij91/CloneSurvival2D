using UnityEngine;

public class VfxUnit : PoolingObject
{
    public void OnAnimationComplete()
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