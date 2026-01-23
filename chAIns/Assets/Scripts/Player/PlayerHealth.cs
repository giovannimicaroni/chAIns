using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public int maxHealth = 100;
    public int currentHealth;
    public Animator anim;
    public GameObject canva;

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

        if(currentHealth <= 0)
        {
            StartCoroutine(Die());
        }

    }

    private IEnumerator Die()
    {
        anim.SetBool("isDead", true);
        canva.SetActive(true);
        yield return new WaitForSeconds(1);
        Time.timeScale = 0;
        
    }

    void Update()
    {
    }
}
