using System;
using TMPro;
using UnityEngine;

public class ResourceNearByOverlay : MonoBehaviour
{
    private ResourceGeneratorData _resourceGeneratorData;
    

    private void Awake()
    {
        Hide();
    }

    private void Update()
    {
        var transform1 = transform;
        var nearbyResourceAmount =
            ResourceGenerator.GetNearbyResourceAmount(_resourceGeneratorData,
                transform1.position + transform1.localPosition);
        var percent = Mathf.RoundToInt((float)nearbyResourceAmount / _resourceGeneratorData.maxResourceAmount * 100f);
        transform.Find("text").GetComponent<TextMeshPro>().SetText(percent + "%");
    }

    public void Show(ResourceGeneratorData resourceGeneratorData)
    {
        _resourceGeneratorData = resourceGeneratorData;
        gameObject.SetActive(true);
        transform.Find("icon").GetComponent<SpriteRenderer>().sprite = _resourceGeneratorData.resourceType.sprite;
        enabled = true;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        enabled = false;
    }

}