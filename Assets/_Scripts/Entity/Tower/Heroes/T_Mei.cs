using UnityEngine;
using System.Collections;

[RequireComponent(typeof(T_MeiWeapon))]
public class T_Mei : OffensiveTower
{
    protected T_MeiWeapon weapon;

    new public void Start()
    {
        base.Start();

        weapon = GetComponent<T_MeiWeapon>();
        weapon.Init(gameObject, tag, attacker.targetTag);
    }

    new public void Update()
    {
        base.Update();
        if(isDead) return;

        if(attacker.target && weapon.IsReadyToFire())
        {
            weapon.Fire(weapon.LeadTarget(attacker.target));
        }
    }
}
