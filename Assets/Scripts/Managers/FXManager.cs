using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class FXManager : Singleton<FXManager>
{
    public SFXClipDatabase sfxDatabase;

    [Range(0f, 1f)] public float sfxVolume, bgmVolume;

    public AudioSource bgmAudioSource;

    private readonly List<SfxUnit> sfxUnits = new List<SfxUnit>();

    private void Start()
    {
        bgmAudioSource.volume = bgmVolume;
    }

    public void PlayVfx(Vector3 pos)
    {
        PoolingManager.Instance.Create<VfxUnit>(POOL_TYPE.Vfx, pos, "Hit");
    }

    public void PlaySfx(Enums.SFX_TYPE type)
    {
        var unit = PoolingManager.Instance.Create<SfxUnit>(POOL_TYPE.Sfx, "SfxUnit");

        unit.Play(sfxDatabase.GetSfxClip(type));
    }

    public void PlayBgm(Enums.BGM_TYPE type)
    {
        bgmAudioSource.clip = sfxDatabase.GetBgmClip(type);
        bgmAudioSource.Play();
    }

    public void RegisterSource(SfxUnit unit)
    {
        if (!sfxUnits.Contains(unit))
        {
            sfxUnits.Add(unit);
            unit.UpdateVolume(sfxVolume);
        }
    }

    public void UnregisterSource(SfxUnit unit)
    {
        if (sfxUnits.Contains(unit))
        {
            sfxUnits.Remove(unit);
        }
    }

    public void SetSfxVolume(float volume)
    {
        sfxVolume = volume;
        foreach (var unit in sfxUnits)
        {
            if (unit != null)
                unit.UpdateVolume(sfxVolume);
        }
    }
}