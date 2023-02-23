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
        if (collision.tag == tag || collision.tag == "Untagged")
            return;

        if (collision.tag == "Player" && tag == "Enemy" || collision.tag == "Enemy" && tag == "Player")
        {
            collision.GetComponentInChildren<Health>().attacker = collision.GetComponentInChildren<Health>().gameObject;
            collision.GetComponentInChildren<Health>().Hit(collision.GetComponentInChildren<Health>().damage, new Vector2(10f, 5f));
            //base.OnTriggerEnter2D(collision);
        }
        //else
            //base.OnTriggerEnter2D(collision);
    }
}
