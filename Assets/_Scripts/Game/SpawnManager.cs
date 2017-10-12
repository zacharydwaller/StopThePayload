using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform spawnPoint;
    public SpawnList[] spawnList;

    protected int round;

    protected float spawnDelay = 1f;
    protected float nextSpawn;
    protected bool currentlySpawning;

    public void Start()
    {
        currentlySpawning = false;
    }

    public void Update()
    {
        if(currentlySpawning && Time.time >= nextSpawn)
        {
            SpawnNextCreep();
        }
    }

    public void StartRound(int roundIndex)
    {
        currentlySpawning = true;
        nextSpawn = Time.time;
        round = roundIndex;
    }

    public void SpawnNextCreep()
    {
        GameObject creepToSpawn;

        creepToSpawn = spawnList[round].GetNext();
        if(creepToSpawn != null)
        {
            Instantiate(creepToSpawn, spawnPoint.position, Quaternion.identity);
            nextSpawn = Time.time + spawnDelay;
        }
        else
        {
            currentlySpawning = false;
        }
    }
}
