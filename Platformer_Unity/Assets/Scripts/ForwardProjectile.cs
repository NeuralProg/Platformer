using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardProjectile : Projectile
{
    public float orientationX;
    private GameObject characterAttached;

    protected override void Start()
    {
        base.Start();

        characterAttached = transform.parent.GetComponent<ShootProjectile>().character;
        orientationX = characterAttached.transform.localScale.x;
        projectileDirection = projectileSpeed * orientationX;
    }
    
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == characterAttached.tag || collision.tag == "Untagged")
            return;

        if (collision.tag == "Player" && characterAttached.tag == "Enemy" || collision.tag == "Enemy" && characterAttached.tag == "Player")
        {
            collision.GetComponent<Health>().attacker = collision.GetComponent<Health>().gameObject;
            collision.GetComponent<Health>().Hit(collision.GetComponent<Health>().damage, new Vector2(10f, 5f));
            base.OnTriggerEnter2D(collision);
        }
        else
            base.OnTriggerEnter2D(collision);
    }
}
