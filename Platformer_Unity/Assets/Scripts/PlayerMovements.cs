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
    private float jumpHeight = 600f;
    private int jumpCount;
    private int jump;

    // Dash
    private float dashLength = 150f;
    private int isDashing;
    private int canDash;
    private float dashTimer;

    // Walled
    [Header("Check wall")]
    [SerializeField] private LayerMask whatIsWall;
    [SerializeField] private Transform checkWall;
    private float wallSlidingSpeed;
    private bool isWallSliding = false;
    private int wallJumping = 0;
    private float wallJumpTimer;

    // Attack
    [Header("Slash objects")]
    [SerializeField] private GameObject slashFront;
    [SerializeField] private GameObject slashDown;
    [SerializeField] private GameObject slashUp;
    public bool isAttacking;
    public float attackTimer;
    #endregion


    #region Basic
    protected override void Start()
    {
        base.Start();

        moveSpeed = 250f;
        jumpCount = 1;
        wallSlidingSpeed = 1f;

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
        rb.velocity = new Vector2((direction * moveSpeed * canMove * Time.fixedDeltaTime) + (transform.localScale.x * dashLength * isDashing * Time.fixedDeltaTime) + (transform.localScale.x * wallJumping * 100f * Time.fixedDeltaTime), (rb.velocity.y * wallSlidingSpeed) + (jump * jumpHeight * Time.fixedDeltaTime) + (transform.localScale.x * wallJumping * 20f * Time.fixedDeltaTime));
        jump = 0;
    }

    private void WallJump()
    {
        wallJumpTimer = 0.1f;
        wallJumping = 1;
        canMove = 0;

        facing = -facing;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        rb.velocity = new Vector2(0f, 0f);
    }

    private void Attack(string attackDirection)
    {
        if (attackDirection == "front")
        {
            slashFront.SetActive(true);
        }

        if (attackDirection == "down")
        {
            slashDown.SetActive(true);
        }

        if (attackDirection == "up")
        {
            slashUp.SetActive(true);
        }
    }

    #endregion


    #region Functions
    protected override void HandleMovement()
    {
        base.HandleMovement();
    }

    private void ResetMechanics()
    {
        jumpCount = 1;
        if (isDashing == 0)
            canDash = 1;
    }

    private void HandleJump()
    {
        if (isFalling)
            anim.ResetTrigger("Jump");

        if (UnityEngine.Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            if (isWallSliding)
            {
                WallJump();
            }
            else
            {
                if (!isGrounded)
                    rb.velocity = new Vector2(rb.velocity.x, 0f);
                jump = 1;
                jumpCount--;
            }
            anim.SetTrigger("Jump");
        }
        if (UnityEngine.Input.GetButtonUp("Jump") == true)
        {
            jump = 0;
            if (!isFalling)
                rb.velocity = new Vector2(rb.velocity.x, 0f);
            anim.ResetTrigger("Jump");
        }

        // Wall jump
        if (wallJumping == 1)
        {
            wallJumpTimer -= Time.deltaTime;
            if (wallJumpTimer <= 0)
            {
                wallJumping = 0;
                canMove = 1;
            }

        }
    }

    private void HandleDash()
    {
        if (UnityEngine.Input.GetButtonDown("Dash") && canDash == 1 && !isWallSliding && !isAttacking)
        {
            canDash = 0;
            canMove = 0;
            isDashing = 1;
            dashTimer = 0.2f;
            anim.SetTrigger("Dash");
        }

        if (isDashing == 1)
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0)
            {
                canMove = 1;
                isDashing = 0;
                anim.ResetTrigger("Dash");
            }
        }
    }

    private void HandleWallSlide()
    {
        var isOnWall = Physics2D.OverlapCircle(checkWall.position, 0.08f, whatIsWall);
        if (isOnWall && !isGrounded)
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
        if (UnityEngine.Input.GetButtonDown("Attack") && !isAttacking && !isWallSliding)
        {
            isAttacking = true;
            attackTimer = 0.5f;
            anim.SetTrigger("Attack");

            if (verticalDirection == 0)
                Attack("front");

            if (verticalDirection == -1)
            {
                if (isGrounded)
                    Attack("front");
                else
                    Attack("down");
            }

            if (verticalDirection == -1)
                Attack("up");
        }

        if (isAttacking)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0)
            {
                isAttacking = false;
                anim.ResetTrigger("Attack");

                slashFront.SetActive(false);
                slashDown.SetActive(false);
                slashUp.SetActive(false);
            }
        }
    }
    #endregion
}