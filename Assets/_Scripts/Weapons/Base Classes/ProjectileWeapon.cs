using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class ProjectileWeapon : Weapon
{
    public GameObject projectile;
    public float projectileSpeed;

    new public void Fire(GameObject target)
    {
        Vector3 angle = target.transform.position - parent.transform.position;

        Fire(angle);
    }

    new public void Fire(Vector3 direction)
    {
        GameObject projectileObj;
        Projectile projectileScript;

        projectileObj = (GameObject) GameObject.Instantiate(
            projectile,
            parent.transform.position,
            Quaternion.identity);
        projectileScript = projectileObj.GetComponent<Projectile>();

        projectileScript.Init(tag, targetTag, damage, projectileSpeed, range);
        projectileScript.Shoot(direction);
    }

    public Vector3 LeadTarget(GameObject target)
    {
        NavMeshAgent targetNav = target.GetComponent<NavMeshAgent>();
        Vector3 targetPosition = target.transform.position;
        Vector3 projectedLocation;
        Vector3 relativePosition;
        float timeToHit;
        bool useGravity = projectile.GetComponent<Rigidbody>().useGravity;

        timeToHit = Vector3.Distance(transform.position, targetPosition) / projectileSpeed;

        projectedLocation =
            targetPosition + (targetNav.velocity * timeToHit);

        relativePosition = projectedLocation - transform.position;

        if(useGravity)
        {
            relativePosition.y += AdjustForGravity(timeToHit);
        }

        return Quaternion.LookRotation(relativePosition).eulerAngles;
    }

    public float AdjustForGravity(float timeToHit)
    {
        //return (Physics.gravity.y * Mathf.Pow(timeToHit, 2)) / (2 * timeToHit);
        return -(0.5f * Physics.gravity.y * Mathf.Pow(timeToHit, 2));
    }
}
