using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardProjectile : Projectile
{
    protected override void Start()
    {
        base.Start();

        projectileDirection = projectileSpeed * transform.parent.localScale.x;
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == transform.parent.tag || collision.tag == "Untagged")
            return;

        if (collision.tag == "Player" && transform.parent.tag == "Enemy" || collision.tag == "Enemy" && transform.parent.tag == "Player")
        {
            collision.GetComponent<Health>().attacker = gameObject.transform.parent.gameObject;
            collision.GetComponent<Health>().Hit(gameObject.transform.parent.gameObject.GetComponent<Health>().damage, new Vector2(10f, 5f));
            base.OnTriggerEnter2D(collision);
        }
        else
            base.OnTriggerEnter2D(collision);
    }
}
