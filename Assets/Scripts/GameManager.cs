using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;


public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance { get; set; }
    [FormerlySerializedAs("player")] public PlayerController playerController;

    public ExperienceManager ExperienceManager;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}