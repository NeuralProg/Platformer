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
    private float dashDuration;
    private int isDashing = 0;
    private int canDash = 0;
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


    // Gère les mouvements du perso 
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

    // Permettre de pouvoir re effectuer des mechaniques une fois au sol ou sur un mur
    private void ResetMechanics()
    {
        jumpCount = 1;
        if (isDashing == 0)
            canDash = 1;
    }

    // Gérer les inputs et conditions pour le saut
    private void HandleJump()
    {
        if (isFalling)
            anim.ResetTrigger("Jump");

        if (UnityEngine.Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            print("jump ! - " + jumpCount);
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
            dashDuration = 0.4f;
            anim.SetTrigger("Dash");
        }

        if (isDashing == 1)
        {
            dashDuration -= Time.fixedDeltaTime;
            if (dashDuration <= 0 && dashDuration >= -0.4f)
            {
                isDashing = 0;
                canMove = 1;
                anim.ResetTrigger("Dash");
            }
        }
    }

    #endregion
}