using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HitDetection : MonoBehaviour
{
    public bool isPlayer;
    public Vector2 colliderSize;


    private void Start()
    {
        colliderSize = GetComponent<BoxCollider2D>().size;
    }

    private void FixedUpdate()
    {
        if (GetComponent<BoxCollider2D>().size == colliderSize)
        {
            GetComponent<BoxCollider2D>().size = new Vector2(0f, 0f);
        }
        else
        {
            GetComponent<BoxCollider2D>().size = colliderSize;
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        HitCharacter(coll);
    }

    private void OnTriggerStay2D(Collider2D coll)
    {
        HitCharacter(coll);
    }

    private void HitCharacter(Collider2D collision)
    {
        if (isPlayer)
        {
            if (collision.tag == "Enemy")
            {
                if (gameObject.name == "SlashDown")
                {
                    if (collision.GetComponent<Health>().takesDamage)
                    {
                        collision.GetComponent<Health>().attacker = gameObject.transform.parent.gameObject;
                        collision.GetComponent<Health>().Hit(gameObject.transform.parent.gameObject.GetComponent<Health>().damage, new Vector2(0f, 0f));
                    }

                    gameObject.transform.parent.gameObject.GetComponent<PlayerMovements>().ResetMechanics();
                    gameObject.transform.parent.gameObject.GetComponent<PlayerMovements>().Knockback(gameObject.transform.parent.gameObject.transform.position - collision.gameObject.transform.position, new Vector2(4f, 7f));
                }
                else
                {
                    if (collision.GetComponent<Health>().takesDamage)
                    {
                        collision.GetComponent<Health>().attacker = gameObject.transform.parent.gameObject;
                        collision.GetComponent<Health>().Hit(gameObject.transform.parent.gameObject.GetComponent<Health>().damage, new Vector2(10f, 5f));

                        gameObject.transform.parent.gameObject.GetComponent<PlayerMovements>().ResetMechanics();
                        gameObject.transform.parent.gameObject.GetComponent<PlayerMovements>().Knockback(gameObject.transform.parent.gameObject.transform.position - collision.gameObject.transform.position, new Vector2(2f, 1f));
                    }
                    else
                    {
                        gameObject.transform.parent.gameObject.GetComponent<PlayerMovements>().ResetMechanics();
                        gameObject.transform.parent.gameObject.GetComponent<PlayerMovements>().Knockback(gameObject.transform.parent.gameObject.transform.position - collision.gameObject.transform.position, new Vector2(7f, 5f));
                    }
                }
            }
        }
        else
        {
            if (collision.tag == "Player")
            {
                collision.GetComponent<Health>().attacker = gameObject.transform.parent.gameObject;
                collision.GetComponent<Health>().Hit(gameObject.transform.parent.gameObject.GetComponent<Health>().damage, new Vector2(10f, 5f));
            }
        }
    }
}
