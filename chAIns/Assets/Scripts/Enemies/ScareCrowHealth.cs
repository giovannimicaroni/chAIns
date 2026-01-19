using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScareCrowHealth : MonoBehaviour, IDamageable
{
    public float health = 10;

    public void DealDamage(float demage)
    {
        health -= demage;
        
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
