using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityCharacterController;
using UnityEngine;

public class DynamicProjectile : Projectile
{
    private Vector2 orientation;
    private GameObject characterAttached;
    public List<Transform> waypoints;
    private bool arrived;
    private int i = 0;

    protected override void Start()
    {
        base.Start();

        characterAttached = transform.parent.GetComponent<ShootProjectile>().character;
    }


    private void Update()
    {
        print(Vector3.Distance(waypoints[i].position, transform.position) + " | " + i);
        if (Vector3.Distance(waypoints[i].position, transform.position) <= 0.05f)
        {
            if (waypoints.Count - 1 != i)
            {
                i++;
                orientation = (waypoints[i].position - transform.position).normalized;
                projectileDirection = projectileSpeed * orientation;
            }
            else
                i = 0;
        }
        else
        {
            orientation = (waypoints[i].position - transform.position).normalized;
            projectileDirection = projectileSpeed * orientation;
        }
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
