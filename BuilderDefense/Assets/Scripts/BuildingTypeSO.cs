using System;
using UnityEngine;

[CreateAssetMenu(fileName = "1", menuName = "ScriptableObjects/BuildingType", order = 0)]
public class BuildingTypeSO : ScriptableObject
{
    public string nameString;
    public Transform prefab;
    public ResourceGeneratorData resourceGeneratorData;
    public Sprite sprite;
    public float minConstructionRadius;
}