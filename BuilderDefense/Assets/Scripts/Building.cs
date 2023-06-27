using System;
using UnityEngine;

public class Building : MonoBehaviour
{
    private HealthSystem _healthSystem;
    public HealthSystem HealthSystem => _healthSystem;

    private void Awake()
    {
        var buildingType = GetComponent<BuildingTypeHolder>().buildingType;
        _healthSystem = new HealthSystem(buildingType.healthAmountMax);
        _healthSystem.Died += (sender, args) => { Destroy(gameObject); };
    }
}