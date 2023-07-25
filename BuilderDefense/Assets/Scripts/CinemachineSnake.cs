using System;
using Cinemachine;
using UnityEngine;

public class CinemachineSnake : MonoBehaviour
{
    public static CinemachineSnake Instance { get; private set; }
    
    private CinemachineVirtualCamera _virtualCamera;
    private CinemachineBasicMultiChannelPerlin _cinemachineMultiChannelPerlin;
    private float _timer;
    private float _timerMax;
    private float _startIntensity;

    private void Awake()
    {
        if (Instance) Destroy(gameObject);
        else Instance = this;
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
        _cinemachineMultiChannelPerlin = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void Update()
    {
        if (!(_timer < _timerMax)) return;
        _timer += Time.deltaTime;
        var amplitude = Mathf.Lerp(_startIntensity, 0, _timer / _timerMax);
        _cinemachineMultiChannelPerlin.m_AmplitudeGain = amplitude;
    }

    public void SnakeCamera(float intensity, float timerMax)
    {
        _timerMax = timerMax;
        _timer = 0f;
        _startIntensity = intensity;
        _cinemachineMultiChannelPerlin.m_AmplitudeGain = intensity;
    }

}