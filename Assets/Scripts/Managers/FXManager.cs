using System;
using System.Collections.Generic;
using Enums;
using UnityEngine;

public class FXManager : Singleton<FXManager>
{
    public SFXClipDatabase sfxDatabase;
    public AudioSource bgmAudioSource;

    private float _sfxVolume;
    private float _bgmVolume;

    public float SfxVolume
    {
        get => _sfxVolume;
        set
        {
            if (value < 0) throw new ArgumentOutOfRangeException(nameof(value));

            foreach (var unit in sfxUnits)
            {
                if (unit != null)
                    unit.UpdateVolume(value);
            }

            _sfxVolume = value;
        }
    }

    public float BgmVolume
    {
        get => _bgmVolume;
        set
        {
            if (value < 0) throw new ArgumentOutOfRangeException(nameof(value));

            bgmAudioSource.volume = value;
            _bgmVolume = value;
        }
    }

    private readonly List<SfxUnit> sfxUnits = new List<SfxUnit>();
    private Dictionary<VFX_TYPE, string> _vfxFileNames = new Dictionary<VFX_TYPE, string>();

    private void Start()
    {
        _bgmVolume = sfxDatabase.bgmVolume;
        _sfxVolume = sfxDatabase.sfxVolume;
        bgmAudioSource.volume = _bgmVolume;
    }

    public void PlayVfx(Vector3 pos, VFX_TYPE type, float scale = 1f)
    {
        if (!_vfxFileNames.ContainsKey(type))
        {
            string typeToString = Enum.GetName(typeof(VFX_TYPE), type);
            _vfxFileNames.Add(type, typeToString);
        }

        PoolingManager.Instance.Create<VfxUnit>(POOL_TYPE.Vfx, pos, _vfxFileNames[type], null, scale);
    }

    public void PlaySfx(SFX_TYPE type)
    {
        var unit = PoolingManager.Instance.Create<SfxUnit>(POOL_TYPE.Sfx, "SfxUnit");

        unit.Play(sfxDatabase.GetSfxClip(type));
    }

    public void PlayBgm(BGM_TYPE type)
    {
        if (bgmAudioSource.clip == sfxDatabase.GetBgmClip(type))
        {
            return;
        }

        bgmAudioSource.clip = sfxDatabase.GetBgmClip(type);
        bgmAudioSource.Play();
    }

    public void RegisterSource(SfxUnit unit)
    {
        if (!sfxUnits.Contains(unit))
        {
            sfxUnits.Add(unit);
            unit.UpdateVolume(_sfxVolume);
        }
    }

    public void UnregisterSource(SfxUnit unit)
    {
        if (sfxUnits.Contains(unit))
        {
            sfxUnits.Remove(unit);
        }
    }
}