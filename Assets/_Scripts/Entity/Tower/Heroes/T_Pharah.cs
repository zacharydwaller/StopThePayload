using UnityEngine;
using System.Collections;

[RequireComponent(typeof(T_PharahWeapon))]
public class T_Pharah : OffensiveTower
{
    protected T_PharahWeapon weapon;

    new public void Start()
    {
        base.Start();

        weapon = GetComponent<T_PharahWeapon>();
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
