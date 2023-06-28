using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BuildingTypeSelectUI : MonoBehaviour
{
    [SerializeField] private List<BuildingTypeSO> ignoreBuildingTypeList;
    
    private Dictionary<BuildingTypeSO, Transform> _btnTransformDictionary;
    private BuildingManager _buildingManager;
    private Transform _preTransformBuildingType;
    private void Awake()
    {
        _buildingManager = BuildingManager.Instance;
        _btnTransformDictionary = new Dictionary<BuildingTypeSO, Transform>();
        _buildingManager.SelectBuildingType += (sender, so) =>
        {
            UpdateActiveBuildingTypeButton(so);
        };
        
        var btnTemplate = transform.Find("btnTemplate");
        btnTemplate.gameObject.SetActive(false);

        var buildingTypeList = Resources.Load<BuildingTypeListSO>(nameof(BuildingTypeListSO));
        var index = 0;
        foreach (var buildingTypeSo in buildingTypeList.list)
        {
            if(ignoreBuildingTypeList.Contains(buildingTypeSo)) continue;
            var btnTransform = Instantiate(btnTemplate, transform);
            btnTransform.gameObject.SetActive(true);
            btnTransform.Find("Image").GetComponent<Image>().sprite = buildingTypeSo.sprite;
            btnTransform.Find("Selected").gameObject.SetActive(false);

            var offsetAmount = 160f;
            btnTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);
            
            btnTransform.GetComponent<Button>().onClick.AddListener(() =>
            {
                _buildingManager.SetActiveBuildingType(buildingTypeSo);
            });

            // Tooltip
            var tooltipUI = TooltipUI.Instance;
            var mouseEnterExitEvents = btnTransform.GetComponent<MouseEnterExitEvents>();
            mouseEnterExitEvents.MouseEnter += (sender, args) =>
            {
                tooltipUI.Show(buildingTypeSo.nameString + "\n" + buildingTypeSo.GetStringConstructionResourceConstArray());
            };
            mouseEnterExitEvents.MouseExit += (sender, args) =>
            {
                tooltipUI.Hide();
            };
            
            _btnTransformDictionary[buildingTypeSo] = btnTransform;
            index++;
        }
    }

    private void UpdateActiveBuildingTypeButton(BuildingTypeSO buildingTypeSo)
    {
        if (_preTransformBuildingType != null)
        {
            _preTransformBuildingType.Find("Selected").gameObject.SetActive(false);
        }
        if (buildingTypeSo == null)
        {
            _preTransformBuildingType = null;
            return;
        }
        var btnTransform = _btnTransformDictionary[buildingTypeSo];
        btnTransform.Find("Selected").gameObject.SetActive(true);
        _preTransformBuildingType = btnTransform;
    }
}