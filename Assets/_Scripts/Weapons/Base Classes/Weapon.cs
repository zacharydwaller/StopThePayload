using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    protected GameObject parent;

    public float damage;
    public float range;
    public float fireDelay;
    protected float nextFire;

    public int shotsPerMag;
    public float reloadDelay;
    [HideInInspector]
    public int currentShot = 0;
    protected bool isReloading = false;

    protected string targetTag;

    public void Init(GameObject newParent, string newTag, string newTargetTag)
    {
        parent = newParent;
        tag = newTag;
        targetTag = newTargetTag;
    }

    public void Fire(GameObject target)
    {
        // Override me
    }

    public void Fire(Vector3 direction)
    {
        // Override me
    }

    public bool IsReadyToFire()
    {
        if(Time.time <= nextFire)
        {
            return false;
        }

        if(!isReloading)
        {
            if(currentShot < shotsPerMag)
            {
                nextFire = Time.time + fireDelay;
                currentShot++;
                return true;
            }
            else
            {
                isReloading = true;
                currentShot = 0;
                nextFire = Time.time + (reloadDelay - fireDelay);
                return false;
            }
        }
        else
        {
            isReloading = false;
            nextFire = Time.time + fireDelay;
            currentShot++;
            return true;
        }
    }
}
