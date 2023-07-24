using System;
using UnityEngine;

public class BuildingConstruction : MonoBehaviour
{
    public static BuildingConstruction Create(Vector3 position, BuildingTypeSO buildingType)
    {
        var pfBuildingConstruction = Resources.Load<Transform>("pfBuildingConstruction");
        var buildingConstruction = Instantiate(pfBuildingConstruction, position, Quaternion.identity)
            .GetComponent<BuildingConstruction>();
        buildingConstruction.Setup(buildingType);
        return buildingConstruction;
    }

    private float _constructionTimer;
    private float _constructionTimerMax;
    private SpriteRenderer _spriteRenderer;
    private BuildingTypeSO _buildingType;
    private BoxCollider2D _boxCollider2D;
    private BuildingTypeHolder _buildingTypeHolder;
    private Material _constructionMaterial;
    private SoundManager _soundManager;

    private void Awake()
    {
        _soundManager = SoundManager.Instance;
        _spriteRenderer = transform.Find("sprite").GetComponent<SpriteRenderer>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _buildingTypeHolder = GetComponent<BuildingTypeHolder>();
        _constructionMaterial = _spriteRenderer.material;
    }

    private void Update()
    {
        _constructionTimer -= Time.deltaTime;
        _constructionMaterial.SetFloat("_Progress",GetConstructionTimerNormalized());
        if (_constructionTimer <= 0)
        {
            Instantiate(_buildingType.prefab, transform.position, Quaternion.identity);
            _soundManager.PlaySound(SoundManager.Sound.BuildingPlaced);
            Destroy(gameObject);
        }
    }

    private void Setup(BuildingTypeSO buildingType)
    {
        _buildingType = buildingType;

        _constructionTimerMax = _buildingType.constructionTimerMax;
        _constructionTimer = _constructionTimerMax;
        _spriteRenderer.sprite = _buildingType.sprite;

        var boxColliderPrefab = _buildingType.prefab.GetComponent<BoxCollider2D>();
        _boxCollider2D.offset = boxColliderPrefab.offset;
        _boxCollider2D.size = boxColliderPrefab.size;

        _buildingTypeHolder.buildingType = _buildingType;
    }

    public float GetConstructionTimerNormalized()
    {
        return 1 - _constructionTimer / _constructionTimerMax;
    }
}