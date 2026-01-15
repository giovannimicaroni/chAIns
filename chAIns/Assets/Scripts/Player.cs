using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 7f;

    public Rigidbody2D rigidBody;
    public Animator animator;
    public Transform groundCheck;
    public LayerMask groundLayer;

    public float groundCheckRadius = 0.2f;

    private float horizontalInput;
    private bool jumpPressed;
    private bool isGrounded;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        jumpPressed = Input.GetButtonDown("Jump");
        CheckGrounded();
        MovePlayer();
        HandleJump();
        AnimatePlayer();
    }

    // FixedUpdate is called 50x per second - 
    void FixedUpdate()
    {
        
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
}
