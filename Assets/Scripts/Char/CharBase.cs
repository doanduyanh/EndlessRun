using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class CharBase : MonoBehaviour
{
    [SerializeField]
    protected Image heath_UI;
    public float health = 100f;
    public float damage = 5f;
    [NonSerialized]
    public float fullHealth;

    public void Start()
    {
        fullHealth = health;
    }
    public void TakeDamage(float damage)
    {
        health -= damage;

        if (heath_UI != null)
        {
            UpdateHealthUI();
        }
        if (health <= 0)
        {
            OnDie();
        }
    }
    public abstract void OnDie();
    public virtual void UpdateHealthUI()
    {
        heath_UI.fillAmount = health / fullHealth;
    }
}
