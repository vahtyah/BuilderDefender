﻿using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    private int _healthAmount;
    [SerializeField] private int _healthAmountMax;
    public event EventHandler Damaged;
    public event EventHandler Died;
    public event EventHandler Healed;

    private void Awake()
    {
        _healthAmount = _healthAmountMax;
    }

    public void SetHealthAmount(int healthAmountMax)
    {
        _healthAmountMax = healthAmountMax;
        _healthAmount = _healthAmountMax;
    }

    public void Damage(int damageAmount)
    {
        _healthAmount -= damageAmount;
        _healthAmount = Mathf.Clamp(_healthAmount, 0, _healthAmountMax);
        
        OnDamaged();
        
        if (IsDead())
        {
            OnDied();
        }
    }

    public bool IsDead()
    {
        return _healthAmount == 0;
    }

    public int GetHealthAmount()
    {
        return _healthAmount;
    }

    public bool IsFullHealth()
    {
        return _healthAmount == _healthAmountMax;
    }

    public float GetHealthAmountNormalized()
    {
        return (float)_healthAmount / _healthAmountMax;
    }

    protected virtual void OnDamaged()
    {
        Damaged?.Invoke(this, EventArgs.Empty);
    }

    protected virtual void OnDied()
    {
        Died?.Invoke(this, EventArgs.Empty);
    }

    public void Heal(int healAmount)
    {
        _healthAmount += healAmount;
        _healthAmount = Mathf.Clamp(_healthAmount, 0, _healthAmountMax);
        OnHealed();
    }

    public void HealFull()
    {
        _healthAmount = _healthAmountMax;
        OnHealed();
    }

    protected virtual void OnHealed()
    {
        Healed?.Invoke(this, EventArgs.Empty);
    }

    public int GetHealthAmountMax()
    {
        return _healthAmountMax;
    }
}