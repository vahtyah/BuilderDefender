using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceUI : MonoBehaviour
{
    private ResourceManager _resourceManager;
    private Dictionary<ResourceTypeSO, Transform> _resourceTransformDictionary;
    private void Awake()
    {
        _resourceManager = ResourceManager.Instance;
        _resourceTransformDictionary = new Dictionary<ResourceTypeSO, Transform>();
        
        _resourceManager.AmountChange += (sender, so) =>
        {
            UpdateResourceAmount(so);
        };
        
        var resourceTypeList = Resources.Load<ResourceTypeListSO>(nameof(ResourceTypeListSO));
        var resourceTemplate = transform.Find("ResourceTemplate");
        resourceTemplate.gameObject.SetActive(false);
        var index = 0;
        const float offsetAmount = -160f;
        foreach (var resourceType in resourceTypeList.list)
        {
            var resourceTransform = Instantiate(resourceTemplate, transform);
            resourceTransform.gameObject.SetActive(true);
            resourceTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);
            resourceTransform.Find("Image").GetComponent<Image>().sprite = resourceType.sprite;
            _resourceTransformDictionary[resourceType] = resourceTransform;
            UpdateResourceAmount(resourceType);
            index++;
        }
    }

    private void UpdateResourceAmount(ResourceTypeSO resourceType)
    {
        var resourceAmount = _resourceManager.GetResourceAmount(resourceType);
        var resourceTransform = _resourceTransformDictionary[resourceType];
        resourceTransform.Find("Text").GetComponent<TextMeshProUGUI>().SetText(resourceAmount.ToString());
    }
}