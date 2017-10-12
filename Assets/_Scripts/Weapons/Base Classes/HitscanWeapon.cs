using UnityEngine;
using System.Collections;

public class HitscanWeapon : Weapon
{
    public GameObject lineObject;
    public float lineDuration;

    public float scatter;

    public void Start()
    {
        if(lineObject)
            lineObject.GetComponent<LineRenderer>().enabled = false;
    }

    new public void Fire(GameObject target)
    {
        Vector3 angle = target.transform.position - parent.transform.position;
        angle.Normalize();
        Fire(angle);
    }

    new public void Fire(Vector3 direction)
    {
        Ray ray;
        RaycastHit rayHit;

        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");

        ray = new Ray(
            parent.transform.position + (Vector3.up * 0.5f),
            direction);

        if(Physics.Raycast(ray, out rayHit, range))
        {
            if(rayHit.transform.tag.Equals(targetTag) ||
                rayHit.transform.tag.Equals("Shield"))
            {
                rayHit.transform.SendMessage("TakeDamage", damage);
            }
        }

        gameObject.layer = LayerMask.NameToLayer("Default");

        DrawLine(direction);
    }

    public void DrawLine(Vector3 direction)
    {
        if(lineObject)
        {
            GameObject newLineObj;
            LineRenderer line;
            Vector3 startPosition, endPosition;
            Vector3 angle;
            float adjacent;
            float theta;

            startPosition = parent.transform.position + (Vector3.up * 0.5f);
            endPosition = startPosition + (direction * range * 1.25f);

            angle = endPosition - startPosition;
            adjacent = angle.magnitude;

            theta = Random.Range(-scatter, scatter) * Mathf.Deg2Rad;
            angle.x += Mathf.Sin(theta) * range;

            theta = Random.Range(-scatter, scatter) * Mathf.Deg2Rad;
            angle.y += Mathf.Sin(theta) * range;

            theta = Random.Range(-scatter, scatter) * Mathf.Deg2Rad;
            angle.z += Mathf.Sin(theta) * range;

            endPosition = startPosition + angle;

            newLineObj = (GameObject) Instantiate(lineObject);
            line = newLineObj.GetComponent<LineRenderer>();

            line.SetPosition(0, startPosition);
            line.SetPosition(1, endPosition);

            newLineObj.GetComponent<HitscanLine>().Init(lineDuration);
        }
    }
}
