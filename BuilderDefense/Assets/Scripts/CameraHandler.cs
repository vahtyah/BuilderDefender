using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] private float moveSpeed = 30f;
    [SerializeField] private float zoomSpeed = 5f;
    [SerializeField] private float zoomAmount = 2f;
    [SerializeField] private float minOrthographicSize = 10f;
    [SerializeField] private float maxOrthographicSize = 30f;

    private float _orthographicSize;
    private float _targetOrthographicSize;

    private void Start()
    {
        _orthographicSize = cinemachineVirtualCamera.m_Lens.OrthographicSize;
        _targetOrthographicSize = _orthographicSize;
    }

    private void Update()
    {
        HandleMove();
        HandleZoom();
    }

    private void HandleZoom()
    {
        _targetOrthographicSize += -Input.mouseScrollDelta.y * zoomAmount;
        _targetOrthographicSize = Mathf.Clamp(_targetOrthographicSize, minOrthographicSize, maxOrthographicSize);
        _orthographicSize = Mathf.Lerp(_orthographicSize, _targetOrthographicSize, zoomSpeed * Time.deltaTime);
        cinemachineVirtualCamera.m_Lens.OrthographicSize = _orthographicSize;
    }

    private void HandleMove()
    {
        var x = Input.GetAxisRaw("Horizontal");
        var y = Input.GetAxisRaw("Vertical");

        //edgeScrolling 
        var edgeScrollingSize = 30f;
        if (Input.mousePosition.x > Screen.width - edgeScrollingSize)
        {
            x = 1f;
        }

        if (Input.mousePosition.x < edgeScrollingSize)
        {
            x = -1f;
        }

        if (Input.mousePosition.y > Screen.height - edgeScrollingSize)
        {
            y = 1f;
        }

        if (Input.mousePosition.y < edgeScrollingSize)
        {
            y = -1f;
        }

        var moveDir = new Vector3(x, y).normalized;
        transform.position += moveDir * (moveSpeed * Time.deltaTime);
    }
}