using System;using UnityEngine;

public class AnimationCompleteReciever : MonoBehaviour, IAnimationCompleteReciever
{
    public  Action AnimaionCompleteAction;
    
    public void OnAnimationComplete(string animationName)
    {
        AnimaionCompleteAction?.Invoke();
    }
}