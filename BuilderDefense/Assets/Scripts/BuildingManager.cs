using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance { get; private set; }

    public event EventHandler<BuildingTypeSO> SelectBuildingType;

    private Camera _mainCamera;
    private BuildingTypeSO _activeBuildingType;
    private BuildingTypeListSO _buildingTypeListSo;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        _buildingTypeListSo = Resources.Load<BuildingTypeListSO>(nameof(BuildingTypeListSO));
    }

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() && _activeBuildingType != null)
        {
            Instantiate(_activeBuildingType.prefab, UtilsClass.GetMouseWorldPosition(), Quaternion.identity);
        }

        if (Input.GetMouseButtonDown(1))
        {
            SetActiveBuildingType(null);
        }
    }
    

    // ReSharper disable Unity.PerformanceAnalysis
    public void SetActiveBuildingType(BuildingTypeSO buildingType)
    {
        _activeBuildingType = buildingType;
        OnSelectBuildingType(_activeBuildingType);
    }

    protected virtual void OnSelectBuildingType(BuildingTypeSO e)
    {
        SelectBuildingType?.Invoke(this, e);
    }
}