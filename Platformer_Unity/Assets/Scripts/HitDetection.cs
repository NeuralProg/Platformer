using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetection : MonoBehaviour
{
    public bool isPlayer;
    private Vector2 colliderSize;


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
                    collision.GetComponent<Health>().attacker = gameObject.transform.parent.gameObject;
                    collision.GetComponent<Health>().Hit(gameObject.transform.parent.gameObject.GetComponent<Health>().damage, new Vector2(0f, 0f));

                    gameObject.transform.parent.gameObject.GetComponent<PlayerMovements>().ResetMechanics();
                    gameObject.transform.parent.gameObject.GetComponent<PlayerMovements>().Knockback(gameObject.transform.parent.gameObject.transform.position - collision.gameObject.transform.position, new Vector2(2f, 7f));
                }
                else
                {
                    collision.GetComponent<Health>().attacker = gameObject.transform.parent.gameObject;
                    collision.GetComponent<Health>().Hit(gameObject.transform.parent.gameObject.GetComponent<Health>().damage, new Vector2(10f, 5f));
                }
            }
            else
            {
                if (collision.tag == "Knockback Block" && gameObject.name == "SlashDown")
                {
                    gameObject.transform.parent.gameObject.GetComponent<PlayerMovements>().ResetMechanics();
                    gameObject.transform.parent.gameObject.GetComponent<PlayerMovements>().Knockback(new Vector3(0f, Mathf.Abs(gameObject.transform.parent.gameObject.transform.position.y - collision.transform.position.y), 0f), new Vector2(0f, 7f));
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
