using System;
using Enums;
using UnityEngine;

public class SpriteAnimationEffectController :EffectController ,IAnimationCompleteReciever
{
    public Animator animator;
    
    public override void PlayTakeDamageEffect()
    {
        OnPlayTakeDamageEffect?.Invoke();
        animator.SetTrigger("TakeDamage");
    }

    public override void PlayDeathEffect()
    {
        animator.SetTrigger("Death");
    }

    public override void PlayAttackEffect()
    {
    }

    public void OnAnimationComplete(string animationName)
    {
        if (animationName == "Death")
        {
            OnCompleteDeathEffect?.Invoke();
        }
        else if (animationName == "TakeDamage")
        {
            OnCompleteTakeDamageEffect?.Invoke();
        }
    }
}