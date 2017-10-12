using UnityEngine;
using System.Collections;

[RequireComponent(typeof(C_McCreeWeapon))]
public class C_McCree : OffensiveCreep
{
    protected C_McCreeWeapon weapon;

    new public void Start()
    {
        base.Start();
        weapon = GetComponent<C_McCreeWeapon>();
        weapon.Init(gameObject, tag, attacker.targetTag);
    }

    new public void Update()
    {
        base.Update();

        if(attacker.target != null
            && weapon.IsReadyToFire())
        {
            weapon.Fire(attacker.target);
        }
    }
}
