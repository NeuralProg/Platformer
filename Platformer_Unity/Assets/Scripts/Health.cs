using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health of the character")]
    public int maxHealth;
    [HideInInspector] public int health;
    [HideInInspector] public bool dead = false;

    [Header("Damage of character")]
    public int damage = 1;
    public bool takesDamage;

    // Check for hit
    [HideInInspector] public bool canBeAttacked = true;

    // Checks for knockback
    [HideInInspector] public GameObject attacker;
    [HideInInspector] public Transform attackerPosition;
    [HideInInspector] public Vector2 knockbackVelocity;

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
