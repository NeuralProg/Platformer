using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyborgEnemy : BaseCharacter
{
    [Header("Projectile")]
    [SerializeField] private GameObject projectilePrefab;

    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate(); 
    }
}
