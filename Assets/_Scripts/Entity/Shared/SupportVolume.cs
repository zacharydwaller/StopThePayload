using UnityEngine;
using System.Collections;

public class SupportVolume : MonoBehaviour
{
    protected Support parent;

    public void Start()
    {
        parent = GetComponentInParent<Support>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals(parent.tag))
        {
            parent.AddAlly(other.gameObject);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.tag.Equals(parent.tag))
        {
            parent.RemoveAlly(other.gameObject);
        }
    }
}