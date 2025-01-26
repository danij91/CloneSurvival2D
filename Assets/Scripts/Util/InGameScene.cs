using UnityEngine;

public class InGameScene : MonoBehaviour
{
    void Awake()
    {
        GameManager.Instance.Initialize();
        FXManager.Instance.Initialize();
        UIManager.Instance.Initialize();
        ExperienceManager.Instance.Initialize();
        SpawnManager.Instance.Initialize();
    }
}
