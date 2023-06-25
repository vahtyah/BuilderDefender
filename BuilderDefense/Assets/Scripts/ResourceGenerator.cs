using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class ResourceGenerator : MonoBehaviour
{
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
        var results = Physics2D.OverlapCircleAll(transform.position, _resourceGeneratorData.resourceDetectionRadius);
        var nearbyResourceAmount = results.Select(collider2D1 => collider2D1.GetComponent<ResourceNode>()).Count(resourceNode => resourceNode != null 
            && resourceNode.resourceType == _resourceGeneratorData.resourceType);
        nearbyResourceAmount = Mathf.Clamp(nearbyResourceAmount, 0, _resourceGeneratorData.maxResourceAmount);
        if (nearbyResourceAmount == 0)
        {
            enabled = false;
        }
        else
        {
            _timerMax = (_resourceGeneratorData.timerMax / 2f) + _resourceGeneratorData.timerMax
                * (1 - (float)nearbyResourceAmount / _resourceGeneratorData.maxResourceAmount);
        }
        Debug.Log("nearbyResourceAmount = " + nearbyResourceAmount);
        Debug.Log("_timerMax = " + _timerMax);
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
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _resourceGeneratorData.resourceDetectionRadius);
    }
}   