using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyborgEnemy : BaseCharacter
{
    [SerializeField] private GameObject characterHit;

    void Start()
    {
        base.Start();

        isAttacking = true;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();

        characterHit.GetComponent<HitDetection>().isAttacking = isAttacking;
    }
}
