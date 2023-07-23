using System;
using UnityEngine;

public class Building : MonoBehaviour
{
    private HealthSystem _healthSystem;
    private Transform _buildingDemolishBtn;
    private Transform _buildingRepairBtn;

    public HealthSystem HealthSystem => _healthSystem;

    private void Awake()
    {
        _buildingDemolishBtn = transform.Find("pfBuildingDemolishBtn");
        _buildingRepairBtn = transform.Find("pfBuildingRepairBtn");
        HideBuildingDemolishBtn();
        HideBuildingRepairBtn();

        var buildingType = GetComponent<BuildingTypeHolder>().buildingType;
        _healthSystem = GetComponent<HealthSystem>();
        _healthSystem.SetHealthAmount(buildingType.healthAmountMax);
        _healthSystem.Died += (sender, args) => { Destroy(gameObject); };
        _healthSystem.Damaged += (sender, args) => { ShowBuildingRepairBtn(); };
        _healthSystem.Healed += (sender, args) =>
        {
            if (_healthSystem.IsFullHealth())
            {
                HideBuildingRepairBtn();
            }
        };
    }

    private void OnMouseEnter()
    {
        ShowBuildingDemolishBtn();
    }

    private void OnMouseExit()
    {
        if (_buildingDemolishBtn != null)
        {
            _buildingDemolishBtn.gameObject.SetActive(false);
        }
    }

    private void ShowBuildingDemolishBtn()
    {
        HideBuildingDemolishBtn();
    }

    private void HideBuildingDemolishBtn()
    {
        if (_buildingDemolishBtn != null)
        {
            _buildingDemolishBtn.gameObject.SetActive(true);
        }
    }


    private void HideBuildingRepairBtn()
    {
        if (_buildingRepairBtn != null)
        {
            _buildingRepairBtn.gameObject.SetActive(false);
        }
    }

    private void ShowBuildingRepairBtn()
    {
        if (_buildingRepairBtn != null)
        {
            _buildingRepairBtn.gameObject.SetActive(true);
        }
    }
}