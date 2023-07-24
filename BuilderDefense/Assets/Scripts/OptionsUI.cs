using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    [SerializeField] private SoundManager _soundManager;
    [SerializeField] private MusicManager _musicManager;
    private TextMeshProUGUI _soundVolumeText;
    private TextMeshProUGUI _musicVolumeText;

    private void Awake()
    {
        _soundVolumeText = transform.Find("soundVolumeText").GetComponent<TextMeshProUGUI>();
        _musicVolumeText = transform.Find("musicVolumeText").GetComponent<TextMeshProUGUI>();

        transform.Find("soundIncreaseBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            _soundManager.IncreaseVolume();
            UpdateSoundVolumeText();
        });
        transform.Find("soundDecreaseBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            _soundManager.DecreaseVolume();
            UpdateSoundVolumeText();
        });
        transform.Find("musicIncreaseBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            _musicManager.IncreaseVolume();
            UpdateMusicVolumeText();
        });
        transform.Find("musicDecreaseBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            _musicManager.DecreaseVolume();
            UpdateMusicVolumeText();
        });
        transform.Find("mainMenuBtn").GetComponent<Button>().onClick.AddListener(() =>
        { 
            Time.timeScale = 1;
            GameSceneManager.Load(GameSceneManager.Scene.MainMenuScene);
        });
    }

    private void Start()
    {
        UpdateSoundVolumeText();
        UpdateMusicVolumeText();
        gameObject.SetActive(false);
    }

    private void UpdateSoundVolumeText()
    {
        _soundVolumeText.SetText(Mathf.RoundToInt(_soundManager.Volume * 10).ToString());
    }

    private void UpdateMusicVolumeText()
    {
        _musicVolumeText.SetText(Mathf.RoundToInt(_musicManager.Volume * 10).ToString());
    }

    public void ToggleVisible()
    {
        GameObject o;
        (o = gameObject).SetActive(!gameObject.activeSelf);
        Time.timeScale = o.activeSelf ? 0 : 1;
    }
}