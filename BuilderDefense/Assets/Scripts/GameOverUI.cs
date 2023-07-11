using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    private Transform _hqBuilding;
    private void Awake()
    {
        _hqBuilding = BuildingManager.Instance.HqBuilding.transform;
        _hqBuilding.GetComponent<HealthSystem>().Died += (sender, args) =>
        {
            Show();
        };
        transform.Find("retryBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            GameSceneManager.Load(GameSceneManager.Scene.GameScene);
        });

        transform.Find("mainMenuBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            GameSceneManager.Load(GameSceneManager.Scene.MainMenuScene);
        });
        Hide();
    }

    private void Show()
    {
        gameObject.SetActive(true);
        transform.Find("wavesSurvivedText").GetComponent<TextMeshProUGUI>()
            .SetText("You survived " + EnemyWaveManager.Instance.WaveNumber + " waves!");
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}