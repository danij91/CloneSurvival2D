using UnityEngine;

public class VFX : MonoBehaviour
{
    public void OnAnimationEnd()
    {
        Destroy(gameObject); // FX 오브젝트 삭제
    }
}