using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScareCrowHealth : MonoBehaviour, IDamageable, IReward
{
    public int maxHealth = 10;
    public int currentHealt;

    public HealthBar healthBar;

    public int goldReward = 10;

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
            Reward(goldReward);
            Destroy(gameObject);
        }
    }

    public void Reward(int reward)
    {
        Console.WriteLine("passou");
        if (GameManager.Instance != null)
        {
            Console.WriteLine("passou");
            GameManager.Instance.AddGold(reward);
        }
    }
}
