using System;
using DefaultNamespace;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    private Camera _mainCamera;
    private BuildingTypeSO _buildingType;
    private BuildingTypeListSO _buildingTypeListSo;

    private void Awake()
    {
        _buildingTypeListSo = Resources.Load<BuildingTypeListSO>(nameof(BuildingTypeListSO));
        _buildingType = _buildingTypeListSo.list[0];
    }

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(_buildingType.prefab, GetMouseWorldPosition(), Quaternion.identity);
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        var mouseWorldPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;
        return mouseWorldPosition;
    }
}