using UnityEngine;
using System.Collections;

public class SplashVolume : MonoBehaviour
{
    protected SplashProjectile projScript;

    public void Start()
    {
        projScript = GetComponentInParent<SplashProjectile>();
    }

    public void Update()
    {
        if(transform.parent == null) Destroy(gameObject);
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == projScript.targetTag)
        {
            projScript.AddEnemy(other.gameObject);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.tag == projScript.targetTag)
        {
            projScript.RemoveEnemy(other.gameObject);
        }
    }
}
