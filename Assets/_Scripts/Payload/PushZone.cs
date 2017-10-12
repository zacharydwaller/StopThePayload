using UnityEngine;
using System.Collections;

public class PushZone : MonoBehaviour
{
    protected ArrayList creepsInRange;

    protected float checkDelay = 0.33f;
    protected float nextCheck = 0f;

    public void Start()
    {
        creepsInRange = new ArrayList();
    }

    public void Update()
    {
        if(Time.time >= nextCheck)
        {
            CheckCreeps();
            nextCheck = Time.time + checkDelay;
        }
    }

    public void CheckCreeps()
    {
        for(int i = creepsInRange.Count - 1; i >= 0; i--)
        {
            if(i > creepsInRange.Count) continue;
            if(creepsInRange[i] == null)
            {
                creepsInRange.RemoveAt(i);
            }
        }

        if(creepsInRange.Count > 0)
        {
            SendMessageUpwards("Go");
        }
        else
        {
            SendMessageUpwards("Stop");
        }
    }

    public void CreepDied(GameObject creep)
    {
        creepsInRange.Remove(creep);
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Creep"))
        {
            creepsInRange.Add(other.gameObject);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.tag.Equals("Creep"))
        {
            creepsInRange.Remove(other.gameObject);
        }
    }
}
