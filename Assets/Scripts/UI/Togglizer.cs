using UnityEngine;
using UnityEngine.UI;

public class Togglizer : MonoBehaviour
{
    public GameObject toggle_01;
    public GameObject toggle_02;

    private bool isFirst = true;
    private Button btn;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        btn = GetComponent<Button>();
        toggle_01.SetActive(isFirst);
        toggle_02.SetActive(!isFirst);
        btn.onClick.AddListener(Toggle);
    }

    public void Toggle()
    {
        isFirst = !isFirst;
        toggle_01.SetActive(isFirst);
        toggle_02.SetActive(!isFirst);
    }
}
