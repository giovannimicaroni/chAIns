using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator anim;
    public Transform attackTransform;
    public float attackRange = 1.0f;
    public LayerMask attackableMask;
    public float timeBetweenAttacks = 1f;
    public Rigidbody2D rigidBody;
    public float recoilForce = 100;

    private float attackTimer;

    RaycastHit2D[] hits;

    public void Start()
    {
        attackTimer = 0;
    }

    public void Update()
    {
        attackTimer += Time.deltaTime;
    }


    public void Attack()
    {
        if(timeBetweenAttacks > attackTimer)
        {
            return;
        }

        attackTimer = 0f;
        anim.SetBool("isAttacking", true);
        hits = Physics2D.CircleCastAll(attackTransform.position, attackRange, transform.right, 0f, attackableMask);

        for (int i = 0; i < hits.Length; i++)
        {
            IDamageable iDamageable = hits[i].collider.gameObject.GetComponent<IDamageable>();
            
            if(iDamageable != null)
            {
                iDamageable.DealDamage(1);
            }
        }
    }

    public void FinishAttack()
    {
        anim.SetBool("isAttacking", false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackTransform.position, attackRange);
    }
}
