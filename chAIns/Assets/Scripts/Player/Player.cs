using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;

    public float speed = 5f;
    public float jumpForce = 7f;
    public float dashForce = 7f;

    public Rigidbody2D rigidBody;
    public Animator animator;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public PlayerAttack playerAttack;

    public float groundCheckRadius = 0.2f;

    private float horizontalInput;
    private bool jumpPressed;
    private bool isGrounded;
    private bool dashPressed;
    private bool dashAllowed = true;
    private bool isDashing = false;
    private bool attackPressed = false;

    private void Awake()
    {
        currentHealth = maxHealth;
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        jumpPressed = Input.GetButtonDown("Jump");
        dashPressed = Input.GetKeyDown(KeyCode.LeftShift);
        attackPressed = Input.GetKeyDown(KeyCode.E);
        CheckGrounded();

        // Nao quero o jogador fazendo coisa enquanto da o dash. A ordem dentro do if importa, pois MovePlayer e Handle Jump mudam a velocidade do jogador
        if (!isDashing)
        {
            MovePlayer();
            HandleJump();
            StartCoroutine(Dash());
        }

        // Esse if podia ta la dentro, mas nao queria ficar nestando
        if (!isDashing && attackPressed)
        {
            playerAttack.Attack();
        }
        
        AnimatePlayer();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            DealDamage(10);
        }
    }

    // FixedUpdate is called 50x per second - 
    void FixedUpdate()
    {
        
    }

    public void DealDamage(float damage)
    {
        currentHealth -= (int)damage;

        healthBar.SetHealth(currentHealth);

    }

    private void CheckGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void MovePlayer()
    {
        rigidBody.velocity = new Vector2(horizontalInput * speed, rigidBody.velocity.y);
    }

    private void HandleJump()
    {
        if (jumpPressed && isGrounded)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
        }
    }

    private void AnimatePlayer()
    {
        FlipPlayerIfNecessary();
        animator.SetFloat("horizontalSpeed", Mathf.Abs(horizontalInput));
        animator.SetBool("isGrounded", isGrounded);
        animator.SetFloat("verticalSpeed", rigidBody.velocity.y);
    }

    private void FlipPlayerIfNecessary()
    {
        if (horizontalInput > 0 && transform.localScale.x < 0 ||
            horizontalInput < 0 && transform.localScale.x > 0)
        {
            Flip();
        }
    }

    private void Flip()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    private IEnumerator Dash()
    {
        if (dashPressed && dashAllowed)
        {
            dashAllowed = false;
            isDashing = true;

            // Desativa a gravidade
            float originalGravity = rigidBody.gravityScale;
            rigidBody.gravityScale = 0;
            rigidBody.velocity = new Vector2(transform.localScale.x * dashForce , 0);

            yield return new WaitForSeconds(0.1f);

            rigidBody.gravityScale = originalGravity;
            isDashing = false;

            // Delay no proximo dash
            yield return new WaitForSeconds(0.5f);

            dashAllowed = true;

        }
    }
}
