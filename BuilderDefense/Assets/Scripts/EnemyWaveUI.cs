using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyWaveUI : MonoBehaviour
{
    [SerializeField] private EnemyWaveManager enemyWaveManager;

    private TextMeshProUGUI _waveNumberText;
    private TextMeshProUGUI _waveMessengerText;
    private RectTransform _enemyWavePositionIndicatorRectTransform;
    private RectTransform _enemyClosestPositionIndicatorRectTransform;
    private Camera _camera;

    private void Awake()
    {
        _waveNumberText = transform.Find("WaveNumberText").GetComponent<TextMeshProUGUI>();
        _waveMessengerText = transform.Find("WaveMessengerText").GetComponent<TextMeshProUGUI>();
        _enemyWavePositionIndicatorRectTransform =
            transform.Find("EnemyWavePositionIndicator").GetComponent<RectTransform>();
        _enemyClosestPositionIndicatorRectTransform =
            transform.Find("EnemyClosestPositionIndicator").GetComponent<RectTransform>();
    }

    private void Start()
    {
        _camera = Camera.main;
        enemyWaveManager.WaveNumberChanged += (sender, s) => { SetWaveNumberText("Wave " + s); };
    }

    private void Update()
    {
        var positionCamera = _camera.transform.position;
        HandleNextWaveMessenge();
        HandleEnemyWaveSpawnPositionIndicator(positionCamera);
        HandleEnemyClosestPositionIndicator(positionCamera);
    }

    private void HandleNextWaveMessenge()
    {
        var nextWaveSpawnTimer = enemyWaveManager.NextWaveSpawnTimer;
        if (nextWaveSpawnTimer < 0f)
        {
            SetMessengerText("");
        }
        else
        {
            SetMessengerText("Next Wave in " + nextWaveSpawnTimer.ToString("F") + "s");
        }
    }

    private void HandleEnemyWaveSpawnPositionIndicator(Vector3 positionCamera)
    {
        var positionSpawnEnemy = enemyWaveManager.SpawnPosition;
        var dirToNextSpawnPosition = (positionSpawnEnemy - positionCamera).normalized;
        _enemyWavePositionIndicatorRectTransform.anchoredPosition = dirToNextSpawnPosition * 300f;
        _enemyWavePositionIndicatorRectTransform.eulerAngles =
            new Vector3(0, 0, UtilsClass.GetAngleFromVector(dirToNextSpawnPosition));

        var distanceToNextSpawnPosition = Vector3.Distance(positionSpawnEnemy, positionCamera);
        _enemyWavePositionIndicatorRectTransform.gameObject.SetActive(distanceToNextSpawnPosition >
                                                                      _camera.orthographicSize * 1.5f);
    }

    private void HandleEnemyClosestPositionIndicator(Vector3 positionCamera)
    {
        var targetMaxRadius = 999f;
        Enemy targetEnemy = null;
        var collider2Ds = Physics2D.OverlapCircleAll(positionCamera, targetMaxRadius);
        foreach (var collider2D in collider2Ds)
        {
            var enemy = collider2D.gameObject.GetComponent<Enemy>();
            if (enemy == null) continue;
            if (targetEnemy == null) targetEnemy = enemy;
            else if (Vector3.Distance(transform.position, enemy.transform.position) <
                     Vector3.Distance(transform.position, targetEnemy.transform.position))
                targetEnemy = enemy;
        }

        if (targetEnemy != null)
        {
            var positionEnemyClosest = targetEnemy.transform.position;
            var dirToClosestPosition = (positionEnemyClosest - positionCamera).normalized;
            _enemyClosestPositionIndicatorRectTransform.anchoredPosition = dirToClosestPosition * 270f;
            _enemyClosestPositionIndicatorRectTransform.eulerAngles =
                new Vector3(0, 0, UtilsClass.GetAngleFromVector(dirToClosestPosition));

            var distanceToClosestEnemyPosition = Vector3.Distance(positionEnemyClosest, positionCamera);
            _enemyClosestPositionIndicatorRectTransform.gameObject.SetActive(distanceToClosestEnemyPosition >
                                                                             _camera.orthographicSize * 1.5f);
        }
        else
        {
            _enemyClosestPositionIndicatorRectTransform.gameObject.SetActive(false);
        }
    }

    private void SetMessengerText(string messenger)
    {
        _waveMessengerText.SetText(messenger);
    }

    private void SetWaveNumberText(string text)
    {
        _waveNumberText.SetText(text);
    }
}