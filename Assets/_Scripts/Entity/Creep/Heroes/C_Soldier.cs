using UnityEngine;
using System.Collections;

[RequireComponent(typeof(C_SoldierWeapon))]
public class C_Soldier : OffensiveCreep
{
    protected C_SoldierWeapon weapon;

    new public void Start()
    {
        base.Start();
        weapon = GetComponent<C_SoldierWeapon>();
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
