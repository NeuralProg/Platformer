using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCharacter : MonoBehaviour
{
    #region Variables
    // Components
    protected SpriteRenderer sr;
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
    protected bool isKnockbacked;

    // Health
    protected int health;
    protected bool isDead;
    protected float invincibilityTime = 0.5f;

    #endregion


    #region Basic
    protected virtual void Start()
    {
        sr = GetComponent<SpriteRenderer>();
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
        {
            rb.velocity = new Vector2(direction * moveSpeed * canMove * Time.fixedDeltaTime, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0f, Time.deltaTime * 3), rb.velocity.y);
            
        }
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
        health = healthStatus.health;
        healthStatus.canBeAttacked = false;
        sr.color = Color.red;
        StartCoroutine(BlinkOnHit());
        if (healthStatus.knockbackVelocity != new Vector2(0f, 0f))
            Knockback(transform.position - healthStatus.attackerPosition.position, healthStatus.knockbackVelocity);
        StartCoroutine(BecomeAttackable());
    }

    protected IEnumerator BlinkOnHit()
    {
        while (sr.color != Color.white)
        {
            yield return null;
            sr.color = Color.Lerp(sr.color, Color.white, Time.deltaTime * 2);
        }
    }

    public virtual void Knockback(Vector3 knockbackDirection, Vector2 knockbackVelocity)
    {
        isKnockbacked = true;
        rb.velocity = knockbackDirection.normalized * knockbackVelocity;
        StartCoroutine(Unknockback(0.3f));
    }
    #endregion


    #region Functions
    protected virtual void HandleMovement()
    {
        Move();
        Flip();
    }

    private IEnumerator Unknockback(float knockbackDuration)
    {
        yield return new WaitForSeconds(knockbackDuration);
        isKnockbacked = false;
    }

    private IEnumerator BecomeAttackable()
    {
        yield return new WaitForSeconds(invincibilityTime);
        healthStatus.canBeAttacked = true;
    } 
    
    
    #endregion
}
