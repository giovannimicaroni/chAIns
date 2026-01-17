using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator anim;

    public void Attack()
    {
        anim.SetBool("isAttacking", true);
    }

    public void FinishAttack()
    {
        anim.SetBool("isAttacking", false);
    }
}
