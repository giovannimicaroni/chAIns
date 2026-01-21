using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScareCrowHealth : MonoBehaviour, IDamageable
{
    public int maxHealth = 10;
    public int currentHealt;

    public HealthBar healthBar;

    public void Awake()
    {
        currentHealt = maxHealth;
        healthBar.SetHealth(currentHealt);
    }

    public void DealDamage(float damage)
    {
        currentHealt -= (int)damage;
        healthBar.SetHealth(currentHealt);
        
        if(currentHealt <= 0)
        {
            Destroy(gameObject);
        }
    }
}
