using System;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "SFXClipDatabase", menuName = "Database/SFXClipDatabase")]
public class SFXClipDatabase : ScriptableObject
{
    [Range(0, 1)]
    public float sfxVolume, bgmVolume;
    [Serializable]
    public class FixedSfxAudioClip
    {
        public Enums.SFX_TYPE type;
        public AudioClip clip;
    }

    public FixedSfxAudioClip[] vfxClips;

    [Serializable]
    public class BgmAudioClip
    {
        public Enums.BGM_TYPE type;
        public AudioClip clip;
    }
    
    public BgmAudioClip[] bgmClips;
    
    public AudioClip GetSfxClip(Enums.SFX_TYPE sfx)
    {
        foreach (var sfxAudioClip in vfxClips)
        {
            if (sfxAudioClip.type == sfx)
            {
                return sfxAudioClip.clip;
            }
        }

        return null;
    }
    
    public AudioClip GetBgmClip(Enums.BGM_TYPE bgm)
    {
        foreach (var bgmAudioClip in bgmClips)
        {
            if (bgmAudioClip.type == bgm)
            {
                return bgmAudioClip.clip;
            }
        }

        return null;
    }
}