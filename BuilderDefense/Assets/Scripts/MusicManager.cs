using System;
using Unity.VisualScripting;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }
    private AudioSource _audioSource;
    public float Volume { get; private set; } = .5f;

    private void Awake()
    {
        if (Instance) Destroy(gameObject);
        else Instance = this;       
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = Volume;

        Volume = PlayerPrefs.GetFloat("volumeMusic",.5f);
    }

    
    public void IncreaseVolume()
    {
        Volume += .1f;
        Volume = Mathf.Clamp01(Volume);
        _audioSource.volume = Volume;
        PlayerPrefs.SetFloat("volumeMusic",Volume);
    }

    public void DecreaseVolume()
    {
        Volume -= .1f;
        Volume = Mathf.Clamp01(Volume);
        _audioSource.volume = Volume;
        PlayerPrefs.SetFloat("volumeMusic",Volume);
    }
}