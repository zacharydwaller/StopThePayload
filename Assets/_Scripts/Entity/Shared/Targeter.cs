using UnityEngine;
using System.Collections;

public class Targeter : MonoBehaviour
{
    // tankFirst/healerFirst will default to highestHP/lowestHP if no tank/healer present
    public enum TargetingType
    {
        highestHP, lowestHP,
        tankFirst, healerFirst,
        first, last
    }
    
    public TargetingType targetingType;
    [HideInInspector]
    public string targetTag;

    public GameObject target;
    protected ArrayList targetsInSight;

    protected float aiRefreshDelay = 0.33f;
    protected float aiNextRefresh;

    protected bool isTargeting = true;

    public void Start()
    {
        targetsInSight = new ArrayList();
        aiNextRefresh = 0f;
    }

    public void Init(string newTargetTag)
    {
        targetTag = newTargetTag;
    } 

    public void Update()
    {
        if(!isTargeting) return;

        if(Time.time >= aiNextRefresh)
        {
            DetermineTarget();
            aiNextRefresh = Time.time + aiRefreshDelay;
        }

        if(target != null)
        {
            transform.LookAt(target.transform);
        }
    }

    public void setTargetingType(TargetingType newType)
    {
        targetingType = newType;
    }

    public void SetIsTargeting(bool newValue)
    {
        isTargeting = newValue;
    }

    public void DetermineTarget()
    {
        CleanTargetList();

        switch(targetingType)
        {
            case TargetingType.highestHP:
                target = FindHighestHP();
                break;
            case TargetingType.lowestHP:
                target = FindLowestHP();
                break;
            case TargetingType.tankFirst:
                target = FindTank();
                break;
            case TargetingType.healerFirst:
                target = FindHealer();
                break;
            case TargetingType.first:
                target = FindFirst();
                break;
        }
    }

    public GameObject FindHighestHP()
    {
        Entity entityScript;
        GameObject newTarget = null;
        float highestHP = 0f;

        foreach(GameObject entity in targetsInSight)
        {
            if(entity == null || entity.GetComponent<Entity>().isDead) continue;

            entityScript = entity.GetComponent<Entity>();

            if(entityScript.health > highestHP)
            {
                newTarget = entity;
                highestHP = entityScript.health;
            }
        }

        return newTarget;
    }

    public GameObject FindLowestHP()
    {
        Entity entityScript;
        GameObject newTarget = null;
        float lowestHP = Mathf.Infinity;

        foreach(GameObject entity in targetsInSight)
        {
            if(entity == null || entity.GetComponent<Entity>().isDead) continue;

            entityScript = entity.GetComponent<Entity>();

            if(entityScript.health < lowestHP)
            {
                newTarget = entity;
                lowestHP = entityScript.health;
            }
        }

        return newTarget;
    }

    // FindTank and FindHealer should only be used with Towers
    public GameObject FindTank()
    {
        Creep creepBase;

        foreach(GameObject creep in targetsInSight)
        {
            if(creep == null || creep.GetComponent<Entity>().isDead) continue;

            creepBase = creep.GetComponent<Creep>();

            if(creepBase.type == Creep.Type.tank)
            {
                return creep;
            }
        }

        return FindFirst();
    }

    public GameObject FindHealer()
    {
        Creep creepBase;

        foreach(GameObject creep in targetsInSight)
        {
            if(creep == null || creep.GetComponent<Entity>().isDead) continue;

            creepBase = creep.GetComponent<Creep>();

            if(creepBase.type == Creep.Type.healer)
            {
                return creep;
            }
        }

        return FindFirst();
    }

    public GameObject FindFirst()
    {
        foreach(GameObject creep in targetsInSight)
        {
            if(creep == null || creep.GetComponent<Entity>().isDead) continue;

            return creep;
        }

        return null;
    }

    public GameObject FindLast()
    {
        for(int i = targetsInSight.Count - 1; i >= 0; i--)
        {
            if(i >= targetsInSight.Count || targetsInSight[i] == null) continue;

            return (GameObject) targetsInSight[i];
        }

        return null;
    }

    public void CleanTargetList()
    {
        object obj;

        for(int i = targetsInSight.Count - 1; i >= 0; i--)
        {
            if(i >= targetsInSight.Count) continue;

            obj = targetsInSight[i];

            if(obj == null)
            {
                targetsInSight.Remove(obj);
            }
        }
    }

    public bool HasLineOfSight(GameObject target)
    {
        Ray ray;
        RaycastHit rayHit;
        bool result = false;

        if(target == null) return false;

        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");

        ray = new Ray(
            transform.position + (Vector3.up * 0.5f),
            target.transform.position - transform.position);
        if(Physics.Raycast(ray, out rayHit))
        {
            Debug.DrawLine(ray.origin, rayHit.point, Color.cyan, 1.0f);

            if(rayHit.transform == target.transform)
            {
                result = true;
            }
        }

        gameObject.layer = LayerMask.NameToLayer("Default");

        Debug.DrawRay(ray.origin, ray.direction, Color.magenta, 0.1f);

        return result;
    }

    public void TargetVolumeEnter(Collider other)
    {
        if(other.tag.Equals(targetTag))
        {
            targetsInSight.Add(other.gameObject);
        }
    }

    public void TargetVolumeExit(Collider other)
    {
        if(other.tag.Equals(targetTag))
        {
            targetsInSight.Remove(other.gameObject);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if(target) Gizmos.DrawLine(transform.position, target.transform.position);
    }
}
