using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetection : MonoBehaviour
{
    public bool isPlayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isPlayer)
        {
            if (collision.tag == "Enemy")
            {
                if (gameObject.name == "SlashDown")
                {
                    collision.GetComponent<Health>().attacker = gameObject.transform.parent.gameObject;
                    collision.GetComponent<Health>().Hit(gameObject.transform.parent.gameObject.GetComponent<Health>().damage, 0f);

                    gameObject.transform.parent.gameObject.GetComponent<PlayerMovements>().ResetMechanics();
                    gameObject.transform.parent.gameObject.GetComponent<PlayerMovements>().Knockback(gameObject.transform.parent.gameObject.transform.position - collision.gameObject.transform.position, 0.15f);
                }
                else
                {
                    collision.GetComponent<Health>().attacker = gameObject.transform.parent.gameObject;
                    collision.GetComponent<Health>().Hit(gameObject.transform.parent.gameObject.GetComponent<Health>().damage, 0.10f);
                }
            }
            else
            {
                if (collision.tag == "Knockback Block")
                {
                    gameObject.transform.parent.gameObject.GetComponent<PlayerMovements>().ResetMechanics();
                    gameObject.transform.parent.gameObject.GetComponent<PlayerMovements>().Knockback(gameObject.transform.parent.gameObject.transform.position - collision.transform.position, 0.15f);
                }
            }
        }
        else
        {
            if (collision.tag == "Player")
            {
                collision.GetComponent<Health>().attacker = gameObject.transform.parent.gameObject;
                collision.GetComponent<Health>().Hit(gameObject.transform.parent.gameObject.GetComponent<Health>().damage, 0.10f);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isPlayer)
        {
            if (collision.tag == "Enemy")
            {
                if (gameObject.name == "SlashDown")
                {
                    collision.GetComponent<Health>().attacker = gameObject.transform.parent.gameObject;
                    collision.GetComponent<Health>().Hit(gameObject.transform.parent.gameObject.GetComponent<Health>().damage, 0f);

                    gameObject.transform.parent.gameObject.GetComponent<PlayerMovements>().ResetMechanics();
                    gameObject.transform.parent.gameObject.GetComponent<PlayerMovements>().Knockback(gameObject.transform.parent.gameObject.transform.position - collision.gameObject.transform.position, 0.15f);
                }
                else
                {
                    collision.GetComponent<Health>().attacker = gameObject.transform.parent.gameObject;
                    collision.GetComponent<Health>().Hit(gameObject.transform.parent.gameObject.GetComponent<Health>().damage, 0.10f);
                }
            }
            else
            {
                if (collision.tag == "Knockback Block")
                {
                    gameObject.transform.parent.gameObject.GetComponent<PlayerMovements>().ResetMechanics();
                    gameObject.transform.parent.gameObject.GetComponent<PlayerMovements>().Knockback(gameObject.transform.parent.gameObject.transform.position - collision.transform.position, 0.15f);
                }
            }
        }
        else
        {
            if (collision.tag == "Player")
            {
                collision.GetComponent<Health>().attacker = gameObject.transform.parent.gameObject;
                collision.GetComponent<Health>().Hit(gameObject.transform.parent.gameObject.GetComponent<Health>().damage, 0.10f);
            }
        }
    }
}
