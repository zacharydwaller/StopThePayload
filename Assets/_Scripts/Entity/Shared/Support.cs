using UnityEngine;
using System.Collections;

public class Support : MonoBehaviour
{
    public float healAmount;
    public float healDelay;
    protected float nextHeal;

    protected ArrayList alliesInRange;

    public void Start()
    {
        alliesInRange = new ArrayList();
        nextHeal = Time.time + healDelay;
    }

    public void Update()
    {
        if(Time.time >= nextHeal)
        {
            for(int i = alliesInRange.Count - 1; i >= 0; i--)
            {
                GameObject ally = (GameObject) alliesInRange[i];

                if(ally)
                {
                    ally.SendMessage("HealDamage", healAmount);
                }
                else
                {
                    alliesInRange.Remove(ally);
                }
            }

            nextHeal = Time.time + healDelay;
        }
    }

    public void AddAlly(GameObject ally)
    {
        alliesInRange.Add(ally);
    }

    public void RemoveAlly(GameObject ally)
    {
        alliesInRange.Remove(ally);
    }
}
