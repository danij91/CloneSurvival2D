using TMPro;
using UnityEngine.UI;

public class DeadPopup : Popup
{
    public Button btn_again;
    public Button btn_quit;
    public TMP_Text txt_messege;

    protected void Start()
    {
        btn_again.onClick.AddListener(OnClickAgain);
        btn_quit.onClick.AddListener(OnClickQuit);
    }

    private void OnClickAgain()
    {
        ExperienceManager.Instance.Reset();
        GameManager.Instance.playerController.GetComponent<Player>().Reset();
        SpawnManager.Instance.Reset();
        Restore();
    }

    private void OnClickQuit()
    {
        FXManager.Instance.PlayBgm(Enums.BGM_TYPE.TITLE);
        PoolingManager.Instance.RestoreAll();
        SceneManager.Instance.LoadScene("Title");
        Restore();
    }

    protected override void OnUse()
    {
        base.OnUse();
        txt_messege.text =
            $"You are dead. Do you want to play again? \nYour score : <b>{ExperienceManager.Instance.GetScore()}</b>";
    }
}