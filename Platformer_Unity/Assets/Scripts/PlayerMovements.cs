using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;
using UnityEngine.Windows;

public class PlayerMovements : BaseCharacter
{
    #region Variables
    // Vertical input
    private float verticalDirection;

    // Jump
    private float jumpHeight = 500f;
    private int jumpCount;
    private int jump;

    // Dash
    private float dashLength = 150f;
    private bool isDashing;
    private int canDash;
    private float dashTimer = 0.2f;

    // Walled
    [Header("Check wall")]
    [SerializeField] private LayerMask whatIsWall;
    [SerializeField] private Transform checkWall;
    private float wallSlidingSpeed;
    private bool isWallSliding = false;
    private int wallJumping = 0;
    private float wallJumpTimer = 0.1f;

    // Attack
    [Header("Slash objects")]
    [SerializeField] private GameObject slashFront;
    [SerializeField] public GameObject slashDown;
    [SerializeField] private GameObject slashUp;
    private float attackTimer = 0.075f;
    private float attackCooldown = 0.25f;
    private bool canAttack = true;

    #endregion


    #region Basic
    protected override void Awake()
    {
        base.Awake();

        moveSpeed = 250f;
        jumpCount = 1;
        wallSlidingSpeed = 1f;
        invincibilityTime = 2f;

        slashFront.SetActive(false);
        slashDown.SetActive(false);
        slashUp.SetActive(false);
    }

    protected override void Update()
    {
        base.Update();

        direction = UnityEngine.Input.GetAxisRaw("Horizontal");
        verticalDirection = UnityEngine.Input.GetAxisRaw("Vertical");

        if (isGrounded || isWallSliding)
        {
            ResetMechanics();
        }

        HandleJump();
        HandleDash();
        HandleWallSlide();
        HandleAttack();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    #endregion


    #region Mechanics 
    protected override void Move()
    {
        if (!isKnockbacked)
        {
            if (isDashing)
                rb.velocity = new Vector2(transform.localScale.x * dashLength * Time.fixedDeltaTime, 0f);
            else
            {
                rb.velocity = new Vector2((direction * moveSpeed * canMove * Time.fixedDeltaTime) + (transform.localScale.x * wallJumping * 100f * Time.fixedDeltaTime), (rb.velocity.y * wallSlidingSpeed) + (jump * jumpHeight * Time.fixedDeltaTime) + (transform.localScale.x * wallJumping * 20f * Time.fixedDeltaTime));
                jump = 0;
            }
        }
        else
        {
            rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0f, Time.fixedDeltaTime * 3), rb.velocity.y);
        }
    }

    private IEnumerator WallJump()
    {
        wallJumpTimer = 0.1f;
        wallJumping = 1;
        canMove = 0;

        facing = -facing;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        rb.velocity = new Vector2(0f, 0f);

        yield return new WaitForSeconds(wallJumpTimer);

        wallJumping = 0;
        canMove = 1;
    }

    private void Attack()
    {
        if (verticalDirection == 0)
        {
            slashFront.SetActive(true);
        }

        if (verticalDirection == -1)
        {
            if (isGrounded)
            {
                slashFront.SetActive(true);
            }
            else
            {
                slashDown.SetActive(true);
            }
        }

        if (verticalDirection == 1)
        {
            slashUp.SetActive(true);
        }

        StartCoroutine(AttackTimer());
    }

    #endregion


    #region Functions
    protected override void HandleMovement()
    {
        base.HandleMovement();
    }

    public void ResetMechanics()
    {
        jumpCount = 1;
        if (!isDashing)
            canDash = 1;
    }

    private void HandleJump()
    {
        if (isFalling)
            anim.ResetTrigger("Jump");

        if (UnityEngine.Input.GetButtonDown("Jump") && jumpCount > 0 && !isAttacking && !isDashing && !healthStatus.dead && (isGrounded || isFalling))
        {
            if (isWallSliding)
            {
                StartCoroutine(WallJump());
            }
            else
            {
                if (isFalling)
                    rb.velocity = new Vector2(rb.velocity.x, 0f);
                jump = 1;
                jumpCount--;
            }
            anim.SetTrigger("Jump");
        }
    }    

    private void HandleDash()
    {
        if (UnityEngine.Input.GetButtonDown("Dash") && canDash == 1 && !isWallSliding && !isAttacking && !healthStatus.dead)
        {
            canDash = 0;
            canMove = 0;
            isDashing = true;
            anim.SetTrigger("Dash");
            rb.gravityScale = 0;
            StartCoroutine(DashTimer());
        }
    }

    private void HandleWallSlide()
    {
        var isOnWall = Physics2D.OverlapCircle(checkWall.position, 0.08f, whatIsWall);
        if (isOnWall && !isGrounded && !healthStatus.dead)
        {
            isWallSliding = true;
            wallSlidingSpeed = 0.8f * Time.fixedDeltaTime;
        }
        else
        {
            isWallSliding = false;
            wallSlidingSpeed = 1f;
        }
        anim.SetBool("Walled", isWallSliding);
    }

    private void HandleAttack()
    {
        if (UnityEngine.Input.GetButtonDown("Attack") && canAttack && !isWallSliding && !healthStatus.dead)
        {
            isAttacking = true;
            canAttack = false;
            canFlip = false;
            anim.SetTrigger("Attack");
            Attack();
        }
    }

    private IEnumerator DashTimer()
    {
        yield return new WaitForSeconds(dashTimer);
        canMove = 1;
        isDashing = false;
        anim.ResetTrigger("Dash");
        rb.gravityScale = 2;
    }

    private IEnumerator AttackTimer()
    {
        yield return new WaitForSeconds(attackTimer);

        slashFront.SetActive(false);
        slashDown.SetActive(false);
        slashUp.SetActive(false);

        isAttacking = false;
        canFlip = true;
        anim.ResetTrigger("Attack");

        StartCoroutine(AttackCooldown());
    }

    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    #endregion
}