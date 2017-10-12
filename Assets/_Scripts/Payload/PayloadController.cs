using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class PayloadController : MonoBehaviour
{
    public GameObject currentWaypoint;

    public GameObject pushZone;

    protected NavMeshAgent navAgent;

    public void Start()
    {
        GameManager gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        navAgent = GetComponent<NavMeshAgent>();

        currentWaypoint = gameManager.waypointHead;

        navAgent.destination = currentWaypoint.transform.position;
        navAgent.Stop();
    }

    // Go and Stop called by attached PushZone
    public void Go()
    {
        navAgent.Resume();
    }

    public void Stop()
    {
        navAgent.Stop();
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Waypoint"))
        {
            GameObject newWaypoint = currentWaypoint.GetComponent<Waypoint>().next;
            if(newWaypoint)
            {
                currentWaypoint = newWaypoint;
                navAgent.destination = currentWaypoint.transform.position;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        if(navAgent) Gizmos.DrawLine(transform.position, navAgent.destination);
    }
}
