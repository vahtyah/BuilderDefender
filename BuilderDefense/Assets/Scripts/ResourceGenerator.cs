using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class ResourceGenerator : MonoBehaviour
{
    [SerializeField] private ResourceManager resourceManager;
    
    private BuildingTypeSO _buildingTypeSo;
    private float _timer;
    private float _timerMax;

    private void Awake()
    {
        _buildingTypeSo = GetComponent<BuildingTypeHolder>().buildingType;
        _timerMax = _buildingTypeSo.resourceGeneratorData.timerMax;
    }

    private void Update()
    {
        _timer -= Time.deltaTime;
        if (!(_timer <= 0f)) return;
        _timer += _timerMax;
        ResourceManager.Instance.AddResource(_buildingTypeSo.resourceGeneratorData.resourceType, 1);
    }   
}   