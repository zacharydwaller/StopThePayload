using UnityEngine;
using UnityEngine.AI;
using System.Collections;

[RequireComponent (typeof(Follower))]
public class Creep : Entity
{
    public enum Type
    {
        tank, healer, attacker
    }

    public int moneyReward;

    public float speed;

    public Type type;

    [HideInInspector]
    public bool isSlowed = false;
    protected float slowAmount = 0.75f;
    protected float slowDuration = 0.25f;
    protected float slowEndTime;

    protected PlayerController player;

    protected Targeter targeter;
    protected Follower follower;

    protected NavMeshAgent navAgent;

    new public void Start()
    {
        base.Start();

        player =
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        follower = GetComponent<Follower>();
        targeter = GetComponent<Targeter>();

        if(targeter)
        {
            targeter.Init("Tower");
        }

        navAgent = GetComponent<NavMeshAgent>();
        navAgent.speed = speed;
    }

    new public void Update()
    {
        base.Update();
        CheckSlow();
    }

    new public void TakeDamage(float amount)
    {
        base.TakeDamage(amount);

        if(health <= 0f)
        {
            Die();
        }
    }

    public void Die(bool rewardMoney = true)
    {
        if(rewardMoney)
        {
            player.money += moneyReward;
        }

        GameObject pushZone = follower.GetPushZone();
        if(pushZone)
        {
            pushZone.SendMessage("CreepDied", gameObject);
        }

        GameObject.Destroy(gameObject);
    }

    public void Slow()
    {
        if(!isSlowed)
        {
            StartSlow();
        }
        else
        {
            slowEndTime = Time.time + slowDuration;
        }
    }

    public void StartSlow()
    {
        isSlowed = true;
        navAgent.speed *= slowAmount;
        slowEndTime = Time.time + slowDuration;

        healthBarController.SetColor(Color.cyan);
    }

    public void CheckSlow()
    {
        //if(isSlowed) Debug.Log("Time: " + Time.time + " Slow End Time: " + slowEndTime);
        if(Time.time >= slowEndTime)
        {
            EndSlow();
        }
    }

    public void EndSlow()
    {
        isSlowed = false;
        navAgent.speed = speed;
        healthBarController.SetColor(Color.red);
    }
}
