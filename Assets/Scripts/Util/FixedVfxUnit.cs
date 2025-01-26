using UnityEngine;

public class FixedVfxUnit : MonoBehaviour
{
    public AnimationCompleteReciever animationCompleteReceiver;
    public Animation vfxAnim;
    private GameObject _animGameObject;
    private Transform _transform;
    
    private void Start()
    {
        _transform = transform;
        _animGameObject = vfxAnim.gameObject;
        animationCompleteReceiver.AnimaionCompleteAction = StopVfx;
    }
    
    public void PlayVfx(float scale)
    {
        _transform.localScale = Vector3.one * scale;
        _animGameObject.SetActive(true);
        vfxAnim.Play();
    }

    private void StopVfx()
    {
        vfxAnim.Stop();
        _animGameObject.SetActive(false);
    }
}
