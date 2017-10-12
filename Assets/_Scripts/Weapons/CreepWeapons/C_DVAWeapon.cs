using UnityEngine;
using System.Collections;

public class C_DVAWeapon : HitscanWeapon
{ 
    protected int numPellets = 11;

    new public void Fire(GameObject target)
    {
        for(int i = 0; i < numPellets; i++)
        {
            base.Fire(target);
        }

    }
}
