using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerMovements : BaseCharacter
{
    #region Variables
    // Move
    private int canMove = 1;

    // Jump
    private float jumpHeight = 600f;
    private int jumpCount = 1;
    private int jump;

    // Dash
    private float dashLength = 200f;
    private int isDashing = 0;
    private int canDash = 0;
    private float dashTimer;
    #endregion


    #region Basic
    protected override void Start()
    {
        base.Start();

        moveSpeed = 250f;
    }

    protected override void Update()
    {
        base.Update();

        direction = UnityEngine.Input.GetAxisRaw("Horizontal");

        if (isGrounded)
        {
            ResetMechanics();
        }

        HandleJump();
        HandleDash();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    #endregion


    #region Mechanics 
    protected override void Move()
    {
        rb.velocity = new Vector2((direction * moveSpeed * canMove * Time.fixedDeltaTime) + (transform.localScale.x * dashLength * isDashing * Time.fixedDeltaTime), rb.velocity.y + jump * jumpHeight * Time.fixedDeltaTime);
        jump = 0;
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
            if (!isGrounded)
                rb.velocity = new Vector2(rb.velocity.x, 0f);
            jump = 1;
            jumpCount--;
            anim.SetTrigger("Jump");
        }
        if (UnityEngine.Input.GetButtonUp("Jump") == true)
        {
            jump = 0;
            if (!isFalling)
                rb.velocity = new Vector2(rb.velocity.x, 0f);
            anim.ResetTrigger("Jump");
        }
    }

    private void HandleDash()
    {
        if (UnityEngine.Input.GetButtonDown("Dash") && canDash == 1)
        {
            canDash = 0;
            canMove = 0;
            isDashing = 1;
            dashTimer = 0.4f;
            anim.SetTrigger("Dash");
        }

        if (isDashing == 1)
        {
            dashTimer -= Time.fixedDeltaTime;
            if (dashTimer <= 0)
            {
                canMove = 1;
                isDashing = 0;
                anim.ResetTrigger("Dash");
            }
        }
    }

    #endregion
}