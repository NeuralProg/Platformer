using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCharacter : MonoBehaviour
{
    #region Variables
    // Components
    protected Rigidbody2D rb;
    protected Animator anim;

    // Movement
    protected float direction;
    private int facing = 1;
    protected float moveSpeed = 200f;

    [Header ("Checks")]
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform checkGround;
    protected bool isGrounded;
    protected bool isFalling;

    #endregion


    #region Basic
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        direction = Input.GetAxisRaw("Horizontal");
        isGrounded = Physics2D.OverlapCircle(checkGround.position, 0.05f, whatIsGround);

        if (rb.velocity.y < 0)
            isFalling = true;
        else
            isFalling = false;

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
        rb.velocity = new Vector2(direction * moveSpeed * Time.fixedDeltaTime, rb.velocity.y);
    }

    private void Flip()
    {
        if (facing != direction && direction != 0f)
        {
            facing = -facing;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
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
