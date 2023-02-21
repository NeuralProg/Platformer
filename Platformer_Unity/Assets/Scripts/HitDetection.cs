using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetection : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.tag == "Player" || collision.tag == "Enemy") && tag != collision.tag)
            collision.GetComponent<Health>().Hit(gameObject.transform.parent.gameObject.GetComponent<Health>().damage, gameObject.transform.parent.gameObject.transform);
    }
}
