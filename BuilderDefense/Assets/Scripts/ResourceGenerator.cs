using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class ResourceGenerator : MonoBehaviour
{
    public static int GetNearbyResourceAmount(ResourceGeneratorData resourceGeneratorData, Vector3 position)
    {
        var results = Physics2D.OverlapCircleAll(position, resourceGeneratorData.resourceDetectionRadius);
        var nearbyResourceAmount = results.Select(collider2D1 => collider2D1.GetComponent<ResourceNode>()).Count(resourceNode => resourceNode != null 
            && resourceNode.resourceType == resourceGeneratorData.resourceType);
        nearbyResourceAmount = Mathf.Clamp(nearbyResourceAmount, 0, resourceGeneratorData.maxResourceAmount);
        return nearbyResourceAmount;
    }
    
    private ResourceGeneratorData _resourceGeneratorData;
    private float _timer;
    private float _timerMax;

    private void Awake()
    {
        _resourceGeneratorData = GetComponent<BuildingTypeHolder>().buildingType.resourceGeneratorData;
        _timerMax = _resourceGeneratorData.timerMax;
    }

    [Obsolete("Obsolete")]
    private void Start()
    {
        var nearbyResourceAmount = ResourceGenerator.GetNearbyResourceAmount(_resourceGeneratorData, transform.position);
        if (nearbyResourceAmount == 0)
        {
            enabled = false;
        }
        else
        {
            _timerMax = (_resourceGeneratorData.timerMax / 2f) + _resourceGeneratorData.timerMax
                * (1 - (float)nearbyResourceAmount / _resourceGeneratorData.maxResourceAmount);
        }
    }

    private void Update()
    {
        _timer -= Time.deltaTime;
        if (!(_timer <= 0f)) return;
        _timer += _timerMax;
        ResourceManager.Instance.AddResource(_resourceGeneratorData.resourceType, 1);
    }

    private void OnDrawGizmos()
    {
        if (Application.IsPlaying(this))
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _resourceGeneratorData.resourceDetectionRadius);
        }
    }

    public ResourceGeneratorData GetResourceGeneratorData()
    {
        return _resourceGeneratorData;
    }

    public float GetTimerNormalized()
    {
        return _timer / _timerMax;
    }

    public float GetAmountGeneratedPerSecond()
    {
        return 1 / _timerMax;
    }
}   