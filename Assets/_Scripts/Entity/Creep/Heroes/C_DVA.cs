using UnityEngine;
using System.Collections;

[RequireComponent(typeof(C_DVAWeapon))]
public class C_DVA : OffensiveCreep
{
    protected C_DVAWeapon weapon;

    new public void Start()
    {
        base.Start();
        weapon = GetComponent<C_DVAWeapon>();
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
