using System;
using Unity.VisualScripting;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }
    private AudioSource _audioSource;

    private void Awake()
    {
        if (Instance) Destroy(gameObject);
        else Instance = this;
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = Volume;
    }

    public float Volume { get; private set; } = .5f;
    
    public void IncreaseVolume()
    {
        Volume += .1f;
        Volume = Mathf.Clamp01(Volume);
        _audioSource.volume = Volume;
    }

    public void DecreaseVolume()
    {
        Volume -= .1f;
        Volume = Mathf.Clamp01(Volume);
        _audioSource.volume = Volume;
    }
}