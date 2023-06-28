using System;
using UnityEngine;
using UnityEngine.Serialization;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private HealthSystem healthSystem;
    private Transform _barTransform;

    private void Start()
    {
        _barTransform = transform.Find("bar");
        UpdateBar();
        healthSystem.Damaged += (sender, args) => { UpdateBar(); };
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