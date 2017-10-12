using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Attacker))]
public class OffensiveTower : Tower
{
    protected Attacker attacker;

    new public void Start()
    {
        base.Start();

        attacker = GetComponent<Attacker>();
        attacker.Init("Creep");
    }

    new public void Update()
    {
        base.Update();

        if(isDead) attacker.SetIsTargeting(false);
        else attacker.SetIsTargeting(true);
    }
}
