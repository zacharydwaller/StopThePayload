using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Attacker))]
public class OffensiveCreep : Creep
{
    protected Attacker attacker;

    new public void Start()
    {
        base.Start();

        attacker = GetComponent<Attacker>();
        attacker.Init("Tower");
    }
}
