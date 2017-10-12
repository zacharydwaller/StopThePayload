using UnityEngine;
using System.Collections;

public class SplashProjectile : Projectile
{
    public GameObject splashVolume;

    protected ArrayList enemiesInSplash;

    public void Start()
    {
        enemiesInSplash = new ArrayList();
    }

    public void HitObstacle()
    {
        for(int i = enemiesInSplash.Count - 1; i >= 0; i--)
        {
            GameObject enemy = (GameObject) enemiesInSplash[i];

            if(enemy)
            {
                enemy.SendMessage("TakeDamage", damage);
            }
        }

        SafeDestroy();
    }

    public void HitShield(GameObject shield)
    {
        shield.SendMessage("TakeDamage", damage);
        SafeDestroy();
    }

    public void AddEnemy(GameObject enemy)
    {
        enemiesInSplash.Add(enemy);
    }

    public void RemoveEnemy(GameObject enemy)
    {
        enemiesInSplash.Remove(enemy);
    }

    new public void SafeDestroy()
    {
        GameObject.Destroy(splashVolume);
        base.SafeDestroy();
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals(parentTag)) return;
        else if(other.tag.Equals(targetTag)
            || other.tag.Equals("Floor"))
        {
            HitObstacle();
        }
        else if(other.tag.Equals("Shield"))
        {
            HitShield(other.gameObject);
        }
    }
}
