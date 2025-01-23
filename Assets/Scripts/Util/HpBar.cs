using UnityEngine;

public class HpBar2D : MonoBehaviour
{
    public Transform hpBarTransform;

    public void UpdateHp(float currentHp, float maxHp)
    {
        float scale = currentHp / maxHp;
        SetSize(scale);
    }

    public void SetSize(float sizeNormalized)
    {
        hpBarTransform.localScale = new Vector3(sizeNormalized, 1f);
    }
}