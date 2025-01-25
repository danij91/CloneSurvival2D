using UnityEngine;

public class VfxUnit : PoolingObject
{
    public AnimationCompleteReciever animationCompleteReciever;
    
    internal override void OnInitialize(params object[] parameters)
    {
        if (parameters.Length > 0)
        {
            float scale = (float)parameters[0];
            transform.localScale = new Vector3(scale, scale, scale);
        }

        if (animationCompleteReciever is not null)
        {
            animationCompleteReciever.AnimaionCompleteAction = Restore;
        }
    }

    protected override void OnUse()
    {
    }

    protected override void OnRestore()
    {
    }
}