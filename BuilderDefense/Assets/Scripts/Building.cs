using System;
using UnityEngine;

public class Building : MonoBehaviour
{
    private HealthSystem _healthSystem;
    private Transform _buildingDemolishBtn;
    private Transform _buildingRepairBtn;
    private SoundManager _soundManager;

    public HealthSystem HealthSystem => _healthSystem;

    private void Awake()
    {
        //Variable
        _buildingDemolishBtn = transform.Find("pfBuildingDemolishBtn");
        _buildingRepairBtn = transform.Find("pfBuildingRepairBtn");
        _soundManager = SoundManager.Instance;
        _healthSystem = GetComponent<HealthSystem>();
        var buildingType = GetComponent<BuildingTypeHolder>().buildingType;
        _healthSystem.SetHealthAmount(buildingType.healthAmountMax);

        //Setup
        HideBuildingDemolishBtn();
        HideBuildingRepairBtn();

        _healthSystem.Died += (sender, args) =>
        {
            Destroy(gameObject);
            Instantiate(GameAssets.Instance.pfBuildingDestroyedParticles, transform.position,
                Quaternion.identity);
            _soundManager.PlaySound(SoundManager.Sound.BuildingDestroyed);
            CinemachineSnake.Instance.SnakeCamera(10f,.2f);
            ChromaticAberration.Instance.SetWeight(1f);

        };
        _healthSystem.Damaged += (sender, args) =>
        {
            ShowBuildingRepairBtn();
            _soundManager.PlaySound(SoundManager.Sound.BuildingDamaged);
            CinemachineSnake.Instance.SnakeCamera(7f,.15f);
            ChromaticAberration.Instance.SetWeight(1f);
        };
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