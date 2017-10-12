using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour
{
    public float maxHealth;
    public float health;

    protected HealthBarController healthBarController;

    //[HideInInspector]
    public bool isDead = false;

    public void Start()
    {
        health = maxHealth;

        healthBarController = GetComponentInChildren<HealthBarController>();
    }

    public void Update()
    {
        // Override
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
    }

    public void HealDamage(float amount)
    {
        health = Mathf.Min(health + amount, maxHealth);
    }
}
