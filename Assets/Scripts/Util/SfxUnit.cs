using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SfxUnit : PoolingObject
{
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void UpdateVolume(float volume)
    {
        if (audioSource != null)
            audioSource.volume = volume;
    }

    public void Play(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
        StartCoroutine(RestoreAfterPlay());
    }

    private IEnumerator RestoreAfterPlay()
    {
        yield return new WaitWhile(() => audioSource.isPlaying);
        Restore();
    }

    internal override void OnInitialize(params object[] parameters)
    {
    }

    protected override void OnUse()
    {
        if (FXManager.Instance != null)
            FXManager.Instance.RegisterSource(this);
    }

    protected override void OnRestore()
    {
        if (FXManager.Instance != null)
            FXManager.Instance.UnregisterSource(this);
    }
}