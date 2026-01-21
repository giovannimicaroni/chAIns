using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;
    void Awake()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void DealDamage(float damage)
    {
        currentHealth -= (int)damage;

        healthBar.SetHealth(currentHealth);

    }

    void Update()
    {
        
    }
}
