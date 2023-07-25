using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    public enum Sound
    {
        BuildingPlaced,
        BuildingDamaged,
        BuildingDestroyed,
        EnemyDie,
        EnemyHit,
        GameOver
    }

    private AudioSource _audioSource;
    private AudioClip _buildingPlaced;
    private Dictionary<Sound, AudioClip> _soundAudioClipDictionary;
    public float Volume { get; private set; } = .5f;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        
        _audioSource = GetComponent<AudioSource>();
        _soundAudioClipDictionary = new Dictionary<Sound, AudioClip>();
        
        foreach (Sound sound in Enum.GetValues(typeof(Sound)))
        {
            _soundAudioClipDictionary[sound] = Resources.Load<AudioClip>(sound.ToString());
        }
        
        Volume = PlayerPrefs.GetFloat("volumeSound",.5f);
    }

    public void PlaySound(Sound sound)
    {
        _audioSource.PlayOneShot(_soundAudioClipDictionary[sound], Volume);
    }

    public void IncreaseVolume()        
    {
        Volume += .1f;
        Volume = Mathf.Clamp01(Volume);
        PlayerPrefs.SetFloat("volumeSound",Volume);
    }

    public void DecreaseVolume()
    {
        Volume -= .1f;
        Volume = Mathf.Clamp01(Volume);
        PlayerPrefs.SetFloat("volumeSound",Volume);
    }

}