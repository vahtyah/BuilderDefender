using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ConstructionTimerUI : MonoBehaviour
{
    [SerializeField] private BuildingConstruction buildingConstruction;
    private Image _constructionImage;

    private void Awake()
    {
        _constructionImage = transform.Find("mask").Find("image").GetComponent<Image>();
    }

    private void Update()
    {
        _constructionImage.fillAmount = buildingConstruction.GetConstructionTimerNormalized();
    }
}