using System;
using DefaultNamespace;
using Unity.VisualScripting;
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
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() && _activeBuildingType != null && CanSpawnBuilding(_activeBuildingType, UtilsClass.GetMouseWorldPosition()))
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

    private bool CanSpawnBuilding(BuildingTypeSO buildingType, Vector3 position)
    {
        var boxCollider2D = buildingType.prefab.GetComponent<BoxCollider2D>();
        var collider2Ds = Physics2D.OverlapBoxAll(position + (Vector3)boxCollider2D.offset, boxCollider2D.size, 0);
        var isAreaClear = collider2Ds.Length == 0;
        if (!isAreaClear)
        {
            return false;
        }

        collider2Ds = Physics2D.OverlapCircleAll(position, buildingType.minConstructionRadius);
        foreach (var collider2D in collider2Ds)
        {
            var buildingTypeHolder = collider2D.GetComponent<BuildingTypeHolder>();
            if (buildingTypeHolder == null) continue;
            if (buildingTypeHolder.buildingType == buildingType)
            {
                return false;
            }
        }

        var maxConstructionRadius = 25f;
        collider2Ds = Physics2D.OverlapCircleAll(position, maxConstructionRadius);
        foreach (var collider2D in collider2Ds)
        {
            var buildingTypeHolder = collider2D.GetComponent<BuildingTypeHolder>();
            if (buildingTypeHolder != null) return true;
        }

        return false;
    }
}