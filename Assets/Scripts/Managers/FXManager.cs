using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class FXManager : Singleton<FXManager>
{
    public enum SFX_TYPE
    {
        HIT,
        SHOOT,
        LEVEL_UP,
    }
    
    public enum BGM_TYPE
    {
        PLAY,
    }

    public GameObject fxPrefab;
    public SFXClipDatabase sfxDatabase;

    [Range(0f, 1f)] public float sfxVolume, bgmVolume;

    public AudioSource bgmAudioSource;

    private Dictionary<SFX_TYPE, AudioSource> _sfxAudioSources = new();

    private void Start()
    {
        bgmAudioSource.volume = bgmVolume;
        
        foreach (var item in sfxDatabase.vfxClips)
        {
            var audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = item.clip;
            audioSource.playOnAwake = false;
            audioSource.volume = sfxVolume;
            audioSource.enabled = false;
            _sfxAudioSources.Add(item.key, audioSource);
        }
    }

    public void PlayVfx(Vector3 pos)
    {
        Instantiate(fxPrefab, pos, Quaternion.identity);
    }

    public void PlaySfx(SFX_TYPE type)
    {
        AudioSource audioSource = _sfxAudioSources[type];
        audioSource.enabled = true;

        audioSource.Play();
    }
    
    public void PlayBgm(BGM_TYPE type)
    {
        bgmAudioSource.clip = sfxDatabase.GetBgmClip(type);
        bgmAudioSource.Play();
    }

    public void SetVolume(float volume)
    {
        foreach (var item in _sfxAudioSources)
        {
            item.Value.volume = volume;
        }
    }
}