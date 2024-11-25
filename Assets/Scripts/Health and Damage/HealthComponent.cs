using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour, IDamageable
{
    [SerializeField] private int MaxHealth;
    private int CurrentHealth;

    private void Start()
    {
        CurrentHealth = MaxHealth;
    }

    //Death event
    public Action OnDeath;
    public void Damage(int n)
    {
        CurrentHealth -= n;
        Debug.Log($"{gameObject.name} took {n} damage");

        if (CurrentHealth <= 0)
            Die();
    }

    public void Die()
    {
        Debug.Log($"{gameObject.name} has died");

        OnDeath?.Invoke();
    }
}
