using UnityEngine;
using System.Collections;

public class T_MeiProjectile : DirectProjectile
{
    new public void HitEnemy(GameObject enemy)
    {
        enemy.SendMessage("Slow");
        base.HitEnemy(enemy);
    }

    public void HitShield(GameObject shield)
    {
        base.HitEnemy(shield);
    }

    new public void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals(parentTag)) return;
        else if(other.tag.Equals(targetTag))
        {
            HitEnemy(other.gameObject);
        }
        else if(other.tag.Equals("Shield"))
        {
            HitShield(other.gameObject);
        }
        else if(other.tag.Equals("Floor")
            || other.tag.Equals("Payload"))
        {
            HitObstacle();
        }
    }
}
