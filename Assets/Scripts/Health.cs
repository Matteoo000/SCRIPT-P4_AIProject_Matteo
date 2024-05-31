using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    public event Action OnDeath; // Declare the OnDeath event

    public float CurrentHealth => currentHealth; // Property to access current health

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log(gameObject.name + " took " + damage + " damage. Current health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        OnDeath?.Invoke(); // Invoke the OnDeath event
        Debug.Log(gameObject.name + " died.");
        Destroy(gameObject);
    }
}
