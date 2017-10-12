using UnityEngine;
using System.Collections;

[RequireComponent(typeof(T_GenjiWeapon))]
public class T_Genji : OffensiveTower
{
    protected T_GenjiWeapon weapon;

    new public void Start()
    {
        base.Start();

        weapon = GetComponent<T_GenjiWeapon>();
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
