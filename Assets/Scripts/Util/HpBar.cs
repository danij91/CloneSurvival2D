using UnityEngine;

public class ProgressBar2D : MonoBehaviour
{
    public Transform hpBarTransform;
    public Color fillColor = Color.red;
    public SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer.color = fillColor;
    }
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