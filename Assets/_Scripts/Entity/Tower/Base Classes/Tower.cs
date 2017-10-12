using UnityEngine;
using System.Collections;

public class Tower : Entity
{
    public int cost;
    public GameObject rangeVisualizer;

    protected float respawnDelay = 8f;
    protected float respawnTime;

    new public void Start()
    {
        base.Start();

        isDead = true;
        respawnTime = Mathf.Infinity;
    }

    new public void Update()
    {
        if(isDead)
        {
            CheckRespawn();
        }
    }

    public void Place()
    {
        rangeVisualizer.SetActive(false);

        isDead = false;
        respawnTime = 0f;
    }

    public void FillHealth()
    {
        if(isDead)
        {
            StopRespawn();
        }

        health = maxHealth;
    }

    new public void TakeDamage(float amount)
    {
        base.TakeDamage(amount);

        if(health <= 0)
        {
            StartRespawn();
        }
    }

    public void StartRespawn()
    {
        isDead = true;
        respawnTime = Time.time + respawnDelay;
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");

        healthBarController.overrideValue = true;
        healthBarController.SetColor(Color.grey);
        healthBarController.SetMaxValue(respawnDelay);
    }

    public void CheckRespawn()
    {
        if(Time.time >= respawnTime)
        {
            StopRespawn();
        }
        else
        {
            healthBarController.SetValue(respawnDelay - (respawnTime - Time.time));
        }
    }

    public void StopRespawn()
    {
        isDead = false;
        health = maxHealth;
        gameObject.layer = LayerMask.NameToLayer("Default");

        healthBarController.overrideValue = false;
        healthBarController.SetColor(Color.red);
        healthBarController.SetMaxValue(maxHealth);
    }

}
