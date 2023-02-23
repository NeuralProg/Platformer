using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    public GameObject character;

    public void Shoot(GameObject projectile, Vector3 pos)
    {
        var bullet = Instantiate(projectile, pos, Quaternion.identity);
        bullet.transform.parent = transform;
    }
}
