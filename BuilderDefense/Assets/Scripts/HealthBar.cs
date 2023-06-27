using System;
using UnityEngine;
using UnityEngine.Serialization;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Building building;
    private HealthSystem _healthSystem;
    private Transform _barTransform;

    private void Start()
    {
        _healthSystem = building.HealthSystem;
        Debug.Log("_healthSystem.GetHealthAmount() = " + _healthSystem.GetHealthAmount());
        _barTransform = transform.Find("bar");
        UpdateBar();
        _healthSystem.Damaged += (sender, args) => { UpdateBar(); };
    }

    private void UpdateBar()
    {
        _barTransform.localScale = new Vector3(_healthSystem.GetHealthAmountNormalized(), 1, 1);
        UpdateHealthBarVisible();
    }

    private void UpdateHealthBarVisible()
    {
        if (_healthSystem.IsFullHealth())
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}