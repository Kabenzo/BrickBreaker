using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float currentHealth { get; private set; }
    bool vulnerable = true; //Can only take damage if this is true

    void Start()
    {
        currentHealth = 2f;
    }
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnBallCollision(collision);

    }


    private void OnBallCollision(Collision2D collision)
    {
        if (vulnerable)
        {
            Ball ball = collision.gameObject.GetComponent<Ball>();
            if (ball != null)
            {
                TakeDamage(1);
            }
        }
    }

    private void TakeDamage(float baseDamage)
    {
        currentHealth -= baseDamage;
        Debug.Log(gameObject.name + " took " + baseDamage + " damage!");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log(gameObject.name + " died!");
        Destroy(gameObject);
    }
}
