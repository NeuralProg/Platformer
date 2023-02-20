using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerMovements : BaseCharacter
{
    #region Variables
    // Jump vars
    public float jumpHeight = 300f;
    private int jumpCount = 1;
    private int jump;
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

        HandleJump();

        if (isGrounded)
        {
            ResetMechanics();
        }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();  
    }
    #endregion


    #region Mechanics 
    protected override void Move()
    {
        rb.velocity = new Vector2(direction * moveSpeed * Time.fixedDeltaTime, rb.velocity.y + jump * jumpHeight * Time.fixedDeltaTime);
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
    #endregion
}
