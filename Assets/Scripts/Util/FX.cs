using UnityEngine;

public class FX : MonoBehaviour
{
    public void OnAnimationEnd()
    {
        Destroy(gameObject); // FX 오브젝트 삭제
    }
    
    public void OnAnimationStart()
    {
        
    }
}