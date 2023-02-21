using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetection : MonoBehaviour
{
    public bool isAttacking;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.tag == "Player" || collision.tag == "Enemy") && tag != collision.tag && isAttacking)
            collision.GetComponent<Health>().Hit(1);
    }
}
