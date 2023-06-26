using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class ResourceGeneratorOverlay : MonoBehaviour
{
    [SerializeField] private ResourceGenerator resourceGenerator;
    private Transform _barTransform;
    private void Start()
    {
        var resourceGeneratorData = resourceGenerator.GetResourceGeneratorData();
        _barTransform = transform.Find("bar");
        var amountGeneratedPerSecond = resourceGenerator.GetAmountGeneratedPerSecond();
        transform.Find("icon").GetComponent<SpriteRenderer>().sprite = resourceGeneratorData.resourceType.sprite;
        transform.Find("text").GetComponent<TextMeshPro>().SetText(amountGeneratedPerSecond.ToString("F"));
    }

    private void Update()
    {
        var timerNormalized = resourceGenerator.GetTimerNormalized();
        _barTransform.localScale = new Vector3(timerNormalized, 1, 1);
    }
    
}