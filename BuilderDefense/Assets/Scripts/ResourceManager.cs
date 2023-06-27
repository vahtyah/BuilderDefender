using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }
    private Dictionary<ResourceTypeSO, int> _resourceAmountDictionary;
    public event EventHandler<ResourceTypeSO> AmountChange;

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

        _resourceAmountDictionary = new Dictionary<ResourceTypeSO, int>();
        var resourceTypeList = Resources.Load<ResourceTypeListSO>(nameof(ResourceTypeListSO));
        foreach (var resourceType in resourceTypeList.list)
        {
            _resourceAmountDictionary[resourceType] = 0;
        }
    }

    private void TestLogResourceAmountDictionary()
    {
        foreach (var resourceTypeSo in _resourceAmountDictionary.Keys)
        {
            Debug.Log(resourceTypeSo.nameString + " : " + _resourceAmountDictionary[resourceTypeSo]);
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public void AddResource(ResourceTypeSO resourceType, int amount)
    {
        _resourceAmountDictionary[resourceType] += amount;
        OnAmountChange(resourceType);
    }

    public int GetResourceAmount(ResourceTypeSO resourceType)
    {
        return _resourceAmountDictionary[resourceType];
    }

    public bool CanAfford(ResourceAmount[] resourceAmounts)
    {
        foreach (var resourceAmount in resourceAmounts)
        {
            if (GetResourceAmount(resourceAmount.resourceType) >= resourceAmount.amount)
            {
            }
            else
            {
                return false;
            }
        }

        return true;
    }

    public void SpendResources(ResourceAmount[] resourceAmounts)
    {
        foreach (var resourceAmount in resourceAmounts)
        {
            _resourceAmountDictionary[resourceAmount.resourceType] -= resourceAmount.amount;
        }
    }

    protected virtual void OnAmountChange(ResourceTypeSO e)
    {
        AmountChange?.Invoke(this, e);
    }
}