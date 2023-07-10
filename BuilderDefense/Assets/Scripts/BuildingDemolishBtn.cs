using System;
using UnityEngine;
using UnityEngine.UI;

public class BuildingDemolishBtn : MonoBehaviour
{
    [SerializeField] private Building _building;
    private void Awake()
    {
        transform.Find("button").GetComponent<Button>().onClick.AddListener(() =>
        {
            var buildingTypeSo = _building.GetComponent<BuildingTypeHolder>().buildingType;
            foreach (var resourceAmount in buildingTypeSo.constructionResourceCostArray)
            {
                ResourceManager.Instance.AddResource(resourceAmount.resourceType, Mathf.FloorToInt(resourceAmount.amount * .6f));
            }
            Destroy(_building.gameObject);
        });
    }
}