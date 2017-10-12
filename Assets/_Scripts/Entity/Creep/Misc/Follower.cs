using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Follower : MonoBehaviour
{
    // Follow priority
    // Tank, if in LoS and follow range
    // Payload, if in LoS and follow range
    // Next waypoint

    public enum FollowState
    {
        none, payload, tank, waypoint
    }

    protected GameObject currentWaypoint;
    protected GameObject closestTank;
    protected GameObject payload;

    public FollowState followState;

    protected float followRange = 12.5f;

    protected float navStopRange = 5f;

    protected float aiRefreshDelay = 0.33f;
    protected float aiNextRefresh;

    protected float sightDistance = 100f;

    protected NavMeshAgent navAgent;
    protected Creep creepBase;

    public void Start()
    {
        GameManager gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        navAgent = GetComponent<NavMeshAgent>();
        creepBase = GetComponent<Creep>();

        aiNextRefresh = 0f;
        followState = FollowState.waypoint;

        currentWaypoint = gameManager.waypointHead;

        payload = gameManager.payload;
    }

    public void Update()
    {
        if(Time.time >= aiNextRefresh)
        {
            RefreshAI();
            SetTarget();
            aiNextRefresh = Time.time + aiRefreshDelay;
        }
    }

    // Calculates follow target
    public void RefreshAI()
    {
        bool hasPayloadLOS, hasTankLOS, isTankSlowed = true;
        float distToPayload, distToTank;

        if(creepBase.type != Creep.Type.tank)
        {
            FindClosestTank();
            hasTankLOS = HasLineOfSight(closestTank);
            distToTank = FindDistanceTo(closestTank);

            if(hasTankLOS)
            {
                isTankSlowed = closestTank.GetComponent<Creep>().isSlowed;
            }
        }
        else
        {
            closestTank = null;
            hasTankLOS = false;
            distToTank = Mathf.Infinity;
        }

        hasPayloadLOS = HasLineOfSight(payload);
        distToPayload = FindDistanceTo(payload);

        if(followState == FollowState.waypoint)
        {
            // tank -> payload if no tank -> waypoint if no payload
            if(hasTankLOS && distToTank <= followRange && !isTankSlowed)
            {
                followState = FollowState.tank;
            }
            else if(hasPayloadLOS && distToPayload <= followRange)
            {
                followState = FollowState.payload;
            }
        }
        else if(followState == FollowState.tank)
        {
            // Payload if no tank or tank slowed -> waypoint if no payload
            if(!hasTankLOS || isTankSlowed)
            {
                if(hasPayloadLOS && distToPayload <= followRange)
                {
                    followState = FollowState.payload;
                }
                else
                {
                    followState = FollowState.waypoint;
                }
            }
        }
        else if(followState == FollowState.payload)
        {
            // tank -> waypoint if no payload los
            if(hasTankLOS && distToTank <= followRange && !isTankSlowed)
            {
                followState = FollowState.tank;
            }
            else if(!hasPayloadLOS)
            {
                followState = FollowState.waypoint;
            }
        }
    }

    public void SetTarget()
    {
        if(followState == FollowState.payload)
        {
            SetDestination(payload.transform);
            navAgent.stoppingDistance = navStopRange;
        }
        else if(followState == FollowState.tank)
        {
            SetDestination(closestTank.transform);
            navAgent.stoppingDistance = navStopRange;
        }
        else if(followState == FollowState.waypoint)
        {
            if(currentWaypoint)
                SetDestination(currentWaypoint.transform);
            else SetDestination(transform);
            navAgent.stoppingDistance = 0f;
        }
        else
        {
            SetDestination(transform);
        }
    }

    public void SetDestination(Transform target)
    {
        navAgent.destination = target.position;
    }

    public void FindClosestTank()
    {
        ArrayList creeps;
        float dist;
        float distToClosest = Mathf.Infinity;

        creeps = ArrayList.Adapter(GameObject.FindGameObjectsWithTag("Creep"));
        closestTank = null;

        foreach(GameObject creep in creeps)
        {
            if(creep == this.gameObject) continue;
            if(creep.GetComponent<Creep>().type != Creep.Type.tank) continue;

            dist = Vector3.Distance(transform.position, creep.transform.position);
            if(dist <= distToClosest)
            {
                distToClosest = dist;
                closestTank = creep;
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

        ray = new Ray(transform.position, target.transform.position - transform.position);
        if(Physics.Raycast(ray, out rayHit))
        {
            if(rayHit.transform == target.transform)
            {
                result = true;
            }
        }

        gameObject.layer = LayerMask.NameToLayer("Default");

        return result;
    }

    public float FindDistanceTo(GameObject target)
    {
        if(target == null) return Mathf.Infinity;

        return Vector3.Distance(transform.position, target.transform.position);
    }

    public GameObject GetPayload()
    {
        if(payload.activeInHierarchy)
            return payload;
        else
            return null;
    }

    public GameObject GetPushZone()
    {
        if(payload.activeInHierarchy)
            return payload.GetComponent<PayloadController>().pushZone;
        else
            return null;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Waypoint"))
        {
            GameObject newWaypoint = other.GetComponent<Waypoint>().next;
            if(newWaypoint)
            {
                currentWaypoint = newWaypoint;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        if(navAgent) Gizmos.DrawLine(transform.position, navAgent.destination);
    }
}
