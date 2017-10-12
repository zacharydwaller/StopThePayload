using UnityEngine;
using System.Collections;

public class HitscanLine : MonoBehaviour
{
    public float duration;
    public float startTime;

    public Color color;

    protected LineRenderer line;

    public void Start()
    {
        line = GetComponent<LineRenderer>();
        line.enabled = true;
    }

    public void Init(float newDuration)
    {
        duration = newDuration;
        startTime = Time.time;

        GameObject.Destroy(gameObject, duration);
    }

    public void Update()
    {
        color.a = ((startTime + duration) - Time.time) / duration;

        line.startColor = color;
        line.endColor = color;
    }
}
