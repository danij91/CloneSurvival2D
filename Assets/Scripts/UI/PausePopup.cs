using UnityEngine.UI;

public class PausePopup : Popup
{
    public Button btn_quit;

    protected void Start()
    {
        btn_quit.onClick.AddListener(OnClickQuit);
    }

    private void OnClickQuit()
    {
        FXManager.Instance.PlayBgm(Enums.BGM_TYPE.TITLE);
        PoolingManager.Instance.RestoreAll();
        SceneManager.Instance.LoadScene("Title");
    }
}