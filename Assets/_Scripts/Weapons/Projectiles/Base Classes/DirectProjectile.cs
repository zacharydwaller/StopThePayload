using UnityEngine;
using System.Collections;

public class DirectProjectile : Projectile
{
    public void HitEnemy(GameObject enemy)
    {
        enemy.SendMessage("TakeDamage", damage);
        SafeDestroy();
    }

    public void HitObstacle()
    {
        SafeDestroy();
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals(parentTag)) return;
        else if(other.tag.Equals(targetTag)
                || other.tag.Equals("Shield"))
        {
            HitEnemy(other.gameObject);
        }
        else if(other.tag.Equals("Floor"))
        {
            HitObstacle();
        }
    }
}
