using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScareCrowDamage : MonoBehaviour
{
    public float knockBackForce = 200;
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IDamageable idamage = collision.gameObject.GetComponent<IDamageable>();
        Player playerScript = collision.gameObject.GetComponent<Player>();
        
        if(idamage != null && playerScript != null)
        {
            idamage.DealDamage(10);

            ContactPoint2D contact = collision.GetContact(0);
            Vector2 normal = contact.normal;

            // Decide de onde veio o contato para ver onde aplicar a forca
            if (Mathf.Abs(normal.x) > Mathf.Abs(normal.y))
            {
                if (normal.x > 0) // Esquerda
                    StartCoroutine(playerScript.KnockBack(-knockBackForce, 0));
                else // Direita
                    StartCoroutine(playerScript.KnockBack(knockBackForce, 0));
            }
            else // Cima
            {
                StartCoroutine(playerScript.KnockBack(0, knockBackForce));
            }
        }
    }
}
