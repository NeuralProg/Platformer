using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public bool dead = false;

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

    public void Hit(int damage)
    {
        health -= damage;
    }
}
