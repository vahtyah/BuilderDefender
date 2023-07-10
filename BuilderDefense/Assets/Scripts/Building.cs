using System;
using UnityEngine;

public class Building : MonoBehaviour
{
    private HealthSystem _healthSystem;
    private Transform _buildingDemolishBtn;

    public HealthSystem HealthSystem => _healthSystem;

    private void Awake()
    {
        _buildingDemolishBtn = transform.Find("pfBuildingDemolishBtn");
        if (_buildingDemolishBtn != null)
        {
            _buildingDemolishBtn.gameObject.SetActive(false);
        }

        var buildingType = GetComponent<BuildingTypeHolder>().buildingType;
        _healthSystem = GetComponent<HealthSystem>();
        _healthSystem.SetHealthAmount(buildingType.healthAmountMax);
        _healthSystem.Died += (sender, args) => { Destroy(gameObject); };
    }

    private void OnMouseEnter()
    {
        if (_buildingDemolishBtn != null)
        {
            _buildingDemolishBtn.gameObject.SetActive(true);
        }
    }

    private void OnMouseExit()
    {
        if (_buildingDemolishBtn != null)
        {
            _buildingDemolishBtn.gameObject.SetActive(false);
        }
    }
}