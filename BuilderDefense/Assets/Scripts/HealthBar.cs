using System;
using UnityEngine;
using UnityEngine.Serialization;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private HealthSystem healthSystem;
    private Transform _barTransform;
    private Transform _separatorContainer;

    private void Start()
    {
        _barTransform = transform.Find("bar");
        _separatorContainer = transform.Find("separatorContainer");

        var barSize = 3f;
        var barOneHealthAmountSize = barSize / healthSystem.GetHealthAmountMax(); //0.02
        
        var separatorTemplate = _separatorContainer.Find("separatorTemplate");
        separatorTemplate.gameObject.SetActive(false);
        var healthSeparatorCount = Mathf.FloorToInt(healthSystem.GetHealthAmountMax() / 10); //15
        for (int i = 0; i < healthSeparatorCount; i++)
        {
            var separatorTransform = Instantiate(separatorTemplate, _separatorContainer);
            separatorTransform.gameObject.SetActive(true);
            separatorTransform.localPosition = new Vector3(barOneHealthAmountSize * i * 10, 0, 0);
        }

        UpdateBar();
        healthSystem.Damaged += (sender, args) => { UpdateBar(); };
        healthSystem.Healed += (sender, args) => { UpdateBar(); };
    }

    private void UpdateBar()
    {
        _barTransform.localScale = new Vector3(healthSystem.GetHealthAmountNormalized(), 1, 1);
        UpdateHealthBarVisible();
    }

    private void UpdateHealthBarVisible()
    {
        if (healthSystem.IsFullHealth())
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}