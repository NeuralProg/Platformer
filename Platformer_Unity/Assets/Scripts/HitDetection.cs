using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetection : MonoBehaviour
{
    public bool isPlayer;

    private Vector2 colliderSize;
    public bool canRefreshTrigger = true;

    private void Start()
    {
        colliderSize = GetComponent<BoxCollider2D>().size;
        InvokeRepeating("StartCoroutine(RefreshTrigger(Time.fixedDeltaTime))", 0f, Time.fixedDeltaTime * 2);
    }

    /*private void Update()
    {
        if (gameObject.name == "SlashDown")
            print(gameObject.activeSelf);

        if (gameObject.activeSelf)
        {
            if (canRefreshTrigger)
            {
                canRefreshTrigger = false;
                StartCoroutine(RefreshTrigger(Time.fixedDeltaTime));
            }
        }
        else
        {
            canRefreshTrigger = true;
        }
    }*/

    private IEnumerator RefreshTrigger(float triggerRefreshRate)
    {
        GetComponent<BoxCollider2D>().size = new Vector2(0f, 0f);
        yield return new WaitForSeconds(triggerRefreshRate);
        GetComponent<BoxCollider2D>().size = colliderSize;
        yield return new WaitForSeconds(triggerRefreshRate);
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
                    collision.GetComponent<Health>().Hit(gameObject.transform.parent.gameObject.GetComponent<Health>().damage, 0f);

                    gameObject.transform.parent.gameObject.GetComponent<PlayerMovements>().ResetMechanics();
                    gameObject.transform.parent.gameObject.GetComponent<PlayerMovements>().Knockback(gameObject.transform.parent.gameObject.transform.position - collision.gameObject.transform.position, 0.2f);
                }
                else
                {
                    collision.GetComponent<Health>().attacker = gameObject.transform.parent.gameObject;
                    collision.GetComponent<Health>().Hit(gameObject.transform.parent.gameObject.GetComponent<Health>().damage, 0.15f);
                }
            }
            else
            {
                if (collision.tag == "Knockback Block" && gameObject.name == "SlashDown")
                {
                    gameObject.transform.parent.gameObject.GetComponent<PlayerMovements>().ResetMechanics();
                    gameObject.transform.parent.gameObject.GetComponent<PlayerMovements>().Knockback(new Vector3(0f, Mathf.Abs(gameObject.transform.parent.gameObject.transform.position.y - collision.transform.position.y), 0f), 0.2f);
                }
            }
        }
        else
        {
            if (collision.tag == "Player")
            {
                collision.GetComponent<Health>().attacker = gameObject.transform.parent.gameObject;
                collision.GetComponent<Health>().Hit(gameObject.transform.parent.gameObject.GetComponent<Health>().damage, 0.15f);
            }
        }
    }
}
