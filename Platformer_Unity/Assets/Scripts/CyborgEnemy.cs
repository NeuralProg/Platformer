using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyborgEnemy : BaseCharacter
{
    [Header ("Hit collision")]
    [SerializeField] private GameObject characterHit;

    void Start()
    {
        base.Start();

        isAttacking = true;
        direction = -1;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();

        characterHit.GetComponent<HitDetection>().isAttacking = isAttacking;
    }
}
