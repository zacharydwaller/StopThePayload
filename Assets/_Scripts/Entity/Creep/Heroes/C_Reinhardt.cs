using UnityEngine;
using System.Collections;

public class C_Reinhardt : OffensiveCreep
{
    public float shieldHealth;

    protected C_ReinhardtShield shield;

    new public void Start()
    {
        base.Start();

        shield = GetComponentInChildren<C_ReinhardtShield>();
        shield.Init(shieldHealth);
    }
}
