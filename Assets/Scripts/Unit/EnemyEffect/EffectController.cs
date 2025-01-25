using System;
using UnityEngine;

public abstract class EffectController : MonoBehaviour
{
    public Action OnPlayTakeDamageEffect;
    public Action OnCompleteTakeDamageEffect;
    public Action OnCompleteDeathEffect;
    

    public abstract void PlayTakeDamageEffect();


    public abstract void PlayDeathEffect();


    public abstract void PlayAttackEffect();
}