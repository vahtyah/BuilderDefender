using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance { get; private set; }

    [SerializeField] private Building _hqBuilding;
    
    public event EventHandler<BuildingTypeSO> SelectBuildingType;
    private ResourceManager _resourceManager;
    private SoundManager _soundManager;
    private Camera _mainCamera;
    private BuildingTypeSO _activeBuildingType;
    private BuildingTypeListSO _buildingTypeListSo;
    private TooltipUI _tooltipUI;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        _tooltipUI = TooltipUI.Instance;
        _resourceManager = ResourceManager.Instance;
        _soundManager = SoundManager.Instance;
        _buildingTypeListSo = Resources.Load<BuildingTypeListSO>(nameof(BuildingTypeListSO));
    }

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() &&
            _activeBuildingType != null)
        {
            if (CanSpawnBuilding(_activeBuildingType, UtilsClass.GetMouseWorldPosition(), out var errorMessage))
            {
                if (_resourceManager.CanAfford(_activeBuildingType.constructionResourceCostArray))
                {
                    BuildingConstruction.Create(UtilsClass.GetMouseWorldPosition(),_activeBuildingType);
                    // Instantiate(_activeBuildingType.prefab, UtilsClass.GetMouseWorldPosition(), Quaternion.identity);
                    _resourceManager.SpendResources(_activeBuildingType.constructionResourceCostArray);
                    _soundManager.PlaySound(SoundManager.Sound.BuildingPlaced);
                }
                else
                {
                    _tooltipUI.Show("Cannot afford " + _activeBuildingType.GetStringConstructionResourceConstArray(),
                        new TooltipUI.TooltipTimer() { timer = 2f });
                }
            }
            else
            {
                _tooltipUI.Show(errorMessage, new TooltipUI.TooltipTimer() { timer = 2f });
            }
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

    private bool CanSpawnBuilding(BuildingTypeSO buildingType, Vector3 position, out string errorMessage)
    {
        var boxCollider2D = buildingType.prefab.GetComponent<BoxCollider2D>();
        var collider2Ds = Physics2D.OverlapBoxAll(position + (Vector3)boxCollider2D.offset, boxCollider2D.size, 0);
        var isAreaClear = collider2Ds.Length == 0;
        if (!isAreaClear)
        {
            errorMessage = "Area is not clear!";
            return false;
        }

        collider2Ds = Physics2D.OverlapCircleAll(position, buildingType.minConstructionRadius);
        foreach (var collider2D in collider2Ds)
        {
            var buildingTypeHolder = collider2D.GetComponent<BuildingTypeHolder>();
            if (buildingTypeHolder == null) continue;
            if (buildingTypeHolder.buildingType == buildingType)
            {
                errorMessage = "To closet to another building of the same type!";
                return false;
            }
        }
        
        if (buildingType.hasResourceGeneratorData)
        {
            var resourceGeneratorData = buildingType.resourceGeneratorData;
            var nearbyResourceAmount = ResourceGenerator.GetNearbyResourceAmount(resourceGeneratorData, position);
            
            if (nearbyResourceAmount == 0)
            {
                errorMessage = "There are no nearby Resource Node!";
                return false;
            }
        }

        var maxConstructionRadius = 25f;
        collider2Ds = Physics2D.OverlapCircleAll(position, maxConstructionRadius);
        foreach (var collider2D in collider2Ds)
        {
            var buildingTypeHolder = collider2D.GetComponent<BuildingTypeHolder>();
            if (buildingTypeHolder != null)
            {
                errorMessage = "";
                return true;
            }
        }

        errorMessage = "Too far from any other building!";
        return false;
    }

    public Building HqBuilding => _hqBuilding;
}