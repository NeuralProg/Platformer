using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCharacter : MonoBehaviour
{
    #region Variables
    // Components
    protected Rigidbody2D rb;
    protected Animator anim;
    protected Health healthStatus;

    // Movement
    protected int canMove = 1;
    protected bool canFlip = true;
    protected float direction;
    protected int facing = 1;
    protected float moveSpeed = 200f;

    // Checks
    [Header ("Check ground")]
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform checkGround;
    protected bool isGrounded;
    protected bool isFalling;

    // Attack
    protected bool isAttacking;
    protected int damage;
    protected float knockbackVelocity = 8f;
    public bool isKnockbacked;

    // Health
    protected int health;
    protected bool isDead;

    #endregion


    #region Basic
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        healthStatus = GetComponent<Health>();

        health = healthStatus.health;
    }

    protected virtual void Update()
    {
        isGrounded = Physics2D.OverlapCircle(checkGround.position, 0.05f, whatIsGround);
        healthStatus.damage = damage;

        if (rb.velocity.y < 0)
            isFalling = true;
        else
            isFalling = false;

        // Check if hit
        if (health > healthStatus.health)
        {
            Hit();
            Knockback(transform.position - healthStatus.attackerPosition.position);
        }

        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        anim.SetBool("Grounded", isGrounded);
        anim.SetBool("Falling", isFalling);
    }

    protected virtual void FixedUpdate()
    {
        HandleMovement();
    }
    #endregion


    #region Mechanics
    protected virtual void Move()
    {
        if (!isKnockbacked)
            rb.velocity = new Vector2(direction * moveSpeed * canMove * Time.fixedDeltaTime, rb.velocity.y);
    }

    private void Flip()
    {
        if (facing != direction && direction != 0f && canMove == 1 && canFlip)
        {
            facing = -facing;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

    protected virtual void Hit()
    {
        anim.SetTrigger("Hit");
        health = healthStatus.health;
    }

    public virtual void Knockback(Vector3 knockbackDirection)
    {
        isKnockbacked = true;
        rb.velocity = knockbackDirection.normalized * knockbackVelocity;
    }
    #endregion


    #region Functions
    protected virtual void HandleMovement()
    {
        Move();
        Flip();
    }
    #endregion
}
