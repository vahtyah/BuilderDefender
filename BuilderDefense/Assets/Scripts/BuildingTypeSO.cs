﻿using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "1", menuName = "ScriptableObjects/BuildingType", order = 0)]
public class BuildingTypeSO : ScriptableObject
{
    public string nameString;
    public Transform prefab;
    public bool hasResourceGeneratorData;
    public ResourceGeneratorData resourceGeneratorData;
    public Sprite sprite;
    public float minConstructionRadius;
    public ResourceAmount[] constructionResourceCostArray;
    public int healthAmountMax;
    public float constructionTimerMax;

    public string GetStringConstructionResourceConstArray()
    {
        return constructionResourceCostArray.Aggregate("", (current, resourceAmount) =>
            current + "<color=#" + resourceAmount.resourceType.colorName.ToHexString() + ">" +
            (resourceAmount.resourceType.nameString + ": " + resourceAmount.amount + "</color>\n"));
    }
}