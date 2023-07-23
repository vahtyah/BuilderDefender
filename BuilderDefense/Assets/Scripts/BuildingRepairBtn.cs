using System;
using UnityEngine;
using UnityEngine.UI;

public class BuildingRepairBtn : MonoBehaviour
{
    [SerializeField] private HealthSystem _healthSystem;
    [SerializeField] private ResourceTypeSO _resourceType;


    private void Awake()
    {
        transform.Find("button").GetComponent<Button>().onClick.AddListener(() =>
        {
            var missingHealth = _healthSystem.GetHealthAmountMax() - _healthSystem.GetHealthAmount();
            var repairCost = missingHealth / 2;
            var resourceAmountCost = new ResourceAmount[]
                { new ResourceAmount { resourceType = _resourceType, amount = repairCost } };
            if (ResourceManager.Instance.CanAfford(resourceAmountCost))
            {
                ResourceManager.Instance.SpendResources(resourceAmountCost);
                _healthSystem.HealFull();
            }
            else
            {
                TooltipUI.Instance.Show("Cannot afford repair cost!",new TooltipUI.TooltipTimer{timer = 2f});
            }

        });
    }
}