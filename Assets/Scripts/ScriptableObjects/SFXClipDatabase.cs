using System;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "SFXClipDatabase", menuName = "Audio/SFXClipDatabase")]
public class SFXClipDatabase : ScriptableObject
{
    [Serializable]
    public class FixedSfxAudioClip
    {
        public Enums.SFX_TYPE key;
        public AudioClip clip;
    }

    public FixedSfxAudioClip[] vfxClips;

    [Serializable]
    public class BgmAudioClip
    {
        public Enums.BGM_TYPE key;
        public AudioClip clip;
    }
    
    public BgmAudioClip[] bgmClips;
    
    public AudioClip GetSfxClip(Enums.SFX_TYPE sfx)
    {
        foreach (var sfxAudioClip in vfxClips)
        {
            if (sfxAudioClip.key == sfx)
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
            if (bgmAudioClip.key == bgm)
            {
                return bgmAudioClip.clip;
            }
        }

        return null;
    }
}