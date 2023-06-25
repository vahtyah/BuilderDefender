﻿using System;
using System.Collections.Generic;
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

    protected virtual void OnAmountChange(ResourceTypeSO e)
    {
        AmountChange?.Invoke(this, e);
    }
}