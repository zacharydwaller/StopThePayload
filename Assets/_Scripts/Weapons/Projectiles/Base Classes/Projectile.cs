using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    protected float damage;
    protected float speed;

    [HideInInspector]
    public string parentTag;
    [HideInInspector]
    public string targetTag;

    protected float lifetime;

    public TrailRenderer trail;

    public void LateUpdate()
    {
        lifetime -= Time.deltaTime;
        if(lifetime <= 0)
        {
            SafeDestroy();
        }
    }

    public void Init(string newParentTag, string newTargetTag, float newDamage, float newSpeed, float range)
    {
        parentTag = newParentTag;
        targetTag = newTargetTag;
        damage = newDamage;
        speed = newSpeed;

        lifetime = range / speed;
    }

    public void Shoot(Vector3 direction)
    {
        Rigidbody _rigidbody = GetComponent<Rigidbody>();
        transform.rotation = Quaternion.Euler(direction);
        _rigidbody.velocity = transform.forward * speed;

        GameObject.Destroy(gameObject, lifetime);
    }

    public void SafeDestroy()
    {
        if(trail)
        {
            GameObject.Destroy(trail.gameObject, trail.time);
            transform.DetachChildren();
        }
        GameObject.Destroy(gameObject);
    }
}
