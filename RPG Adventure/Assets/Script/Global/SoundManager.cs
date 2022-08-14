using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : GenericSingleton<SoundManager>
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] Sound[] sounds;

    public void PlaySFXSound(SoundType soundType)
    {
        AudioClip clip = GetAudioClip(soundType);
        if(clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    private AudioClip GetAudioClip(SoundType soundType)
    {
        Sound sound = Array.Find(sounds, item => item.soundType == soundType);
        if(sound != null)
        {
            return sound.clip;
        }
        return null;
    }

    [System.Serializable]
    public class Sound
    {
        public SoundType soundType;
        public AudioClip clip;
    }
}

public enum SoundType
{
    None,
    ArrowLaunch,
    FireBallLaunch,
    MeleeHit,
    SwordHit,
    TakeDamage,
    WeaponPickUp,
    Dead
}
