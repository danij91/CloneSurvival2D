using UnityEngine;
using UnityEngine.UI;

public class SettingPopup : Popup
{
    public Slider slider_bgm_volume;
    public Slider slider_sfx_volume;

    protected void Start()
    {
        slider_bgm_volume.value = FXManager.Instance.BgmVolume;
        slider_sfx_volume.value = FXManager.Instance.SfxVolume;
        slider_bgm_volume.onValueChanged.AddListener(OnBgmValueChanged);
        slider_sfx_volume.onValueChanged.AddListener(OnSfxValueChanged);
    }

    private void OnBgmValueChanged(float value)
    {
        FXManager.Instance.BgmVolume = value;
    }

    private void OnSfxValueChanged(float value)
    {
        FXManager.Instance.SfxVolume = value;
    }
}