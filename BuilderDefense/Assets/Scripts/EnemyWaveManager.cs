using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EnemyWaveManager : MonoBehaviour
{
    public static EnemyWaveManager Instance { get; private set; }
    private enum State
    {
        WaitingToSpawnNextWave,
        SpawningWave
    }

    public event EventHandler<string> WaveNumberChanged;

    [SerializeField] private List<Transform> spawnPositionTransform;
    [SerializeField] private Transform nextWavePositionTransform;
    
    
    private State _state;
    private float _nextWaveSpawnTimer;
    private float _nextEnemySpawnTimer;
    private int _remainingEnemySpawnAmount;
    private Vector3 _spawnPosition;
    private int _waveNumber;

    public float NextWaveSpawnTimer => _nextWaveSpawnTimer;

    public Vector3 SpawnPosition => _spawnPosition;

    public int WaveNumber => _waveNumber;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        _state = State.WaitingToSpawnNextWave;
        _spawnPosition = spawnPositionTransform[Random.Range(0,spawnPositionTransform.Count)].position;
        nextWavePositionTransform.position = _spawnPosition;
        _nextWaveSpawnTimer = 3f;
    }

    private void Update()
    {
        switch (_state)
        {
            case State.WaitingToSpawnNextWave:
                _nextWaveSpawnTimer -= Time.deltaTime;
                if (_nextWaveSpawnTimer <= 0f)
                {
                    SpawnWave();
                }
                break;
            case State.SpawningWave:
                if (_remainingEnemySpawnAmount > 0)
                {
                    _nextEnemySpawnTimer -= Time.deltaTime;
                    if (_nextEnemySpawnTimer < 0f)
                    {
                        _nextEnemySpawnTimer = Random.Range(0f, .2f);
                        Enemy.Create(_spawnPosition + UtilsClass.GetRandomDir() * Random.Range(0f, 10f));
                        _remainingEnemySpawnAmount--;
                        if (_remainingEnemySpawnAmount <= 0)
                        {
                            _state = State.WaitingToSpawnNextWave;
                            _spawnPosition = spawnPositionTransform[Random.Range(0,spawnPositionTransform.Count)].position;
                            nextWavePositionTransform.position = _spawnPosition;
                            _nextWaveSpawnTimer = 10f;
                        }

                    }
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    void SpawnWave()
    {
        _remainingEnemySpawnAmount = 5 + 3 * _waveNumber;
        _state = State.SpawningWave;
        _waveNumber++;
        OnWaveNumberChanged(_waveNumber.ToString());
    }

    protected virtual void OnWaveNumberChanged(string e)
    {
        WaveNumberChanged?.Invoke(this, e);
    }
}