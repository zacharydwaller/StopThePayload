using UnityEngine;
using System.Collections;

public class C_ReinhardtShield : MonoBehaviour
{
    protected float health;
    protected float maxHealth;

    protected float rechargeDelay = 5f;
    protected float rechargeTime;

    protected bool isBroken;

    protected BoxCollider _collider;
    protected MeshRenderer _renderer;
    protected Light _light;

    public void Start()
    {
        _collider = GetComponent<BoxCollider>();
        _renderer = GetComponent<MeshRenderer>();
        _light = GetComponentInChildren<Light>();
    }

    public void Update()
    {
        if(isBroken && (Time.time >= rechargeTime))
        {
            RepairShield();
        }
    }

    public void Init(float newHealth)
    {
        maxHealth = health = newHealth;
        rechargeTime = 0f;
        isBroken = false;
    }

    public void BreakShield()
    {
        _collider.enabled = false;
        _renderer.enabled = false;
        _light.enabled = false;

        rechargeTime = Time.time + rechargeDelay;
    }

    public void RepairShield()
    {
        _collider.enabled = true;
        _renderer.enabled = true;
        _light.enabled = true;

        health = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        if(health <= 0f)
        {
            BreakShield();
        }
    }
}
