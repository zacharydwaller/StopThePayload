using UnityEngine;
using System.Collections;

public class Attacker : Targeter
{
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //if(target) Gizmos.DrawLine(transform.position, target.transform.position);
        //if(target) Gizmos.DrawSphere(target.transform.position, 1f);
    }
}
