using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyborgEnemy : BaseCharacter
{
    [Header ("Hit collision")]
    [SerializeField] private GameObject characterHit;

    protected override void Start()
    {
        base.Start();

        isAttacking = true;
        direction = -1;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        characterHit.GetComponent<HitDetection>().isAttacking = isAttacking;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
