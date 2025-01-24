using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;


public class GameManager : Singleton<GameManager>
{
    
    public static GameManager Instance { get; set; }
    public PlayerController playerController;
    
    private bool isPaused = false;
    
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

    private void Start()
    {
        FXManager.Instance.PlayBgm(FXManager.BGM_TYPE.PLAY);
    }
    
    public void TogglePause()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
    }
}