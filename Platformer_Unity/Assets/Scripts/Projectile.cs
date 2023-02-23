using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected Vector2 projectileSpeed;
    [SerializeField] protected int projectileDamage;
    protected Rigidbody2D rb;
    protected Vector2 projectileDirection;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = projectileDirection * Time.fixedDeltaTime;
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
