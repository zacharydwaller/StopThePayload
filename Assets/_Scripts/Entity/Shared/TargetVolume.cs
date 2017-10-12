using UnityEngine;
using System.Collections;

public class TargetVolume : MonoBehaviour
{
    protected Targeter targeter;

    public void Start()
    {
        targeter = GetComponentInParent<Targeter>();
    }

    public void OnTriggerEnter(Collider other)
    {
        targeter.TargetVolumeEnter(other);
    }

    public void OnTriggerExit(Collider other)
    {
        targeter.TargetVolumeExit(other);
    }
}
