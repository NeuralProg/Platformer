using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // Health of the character
    public int health;
    public int maxHealth;
    public bool dead = false;
    public bool canBeAttacked = true;

    // Damage the character deals
    public int damage = 1;

    // Checks for knockback
    public GameObject attacker;
    public Transform attackerPosition;
    public Vector2 knockbackVelocity;

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

    public void Hit(int damageTaken, Vector2 knockbackVel)
    {
        if (canBeAttacked)
        {
            health -= damageTaken;
            attackerPosition = attacker.transform;
            knockbackVelocity = knockbackVel;
        }
    }
}
