using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // Health of the character
    public int health;
    public int maxHealth;
    public bool dead = false;

    // Damage the character deals
    public int damage = 1;

    // Checks for knockback
    public Transform attackerPosition;

    void Awake()
    {
        health = maxHealth;
    }

    void Update()
    {
        if (health <= 0)
        {
            dead = true;
        }
    }

    public void Hit(int damageTaken, Transform attackerPos)
    {
        health -= damageTaken;
        attackerPosition = attackerPos;
    }
}
