using UnityEngine;
using System.Collections;

[System.Serializable]
public class SpawnList : System.Object
{
    public GameObject[] spawnedCreeps;
    public int[] amountToSpawn;
    protected int index = 0;

    public GameObject GetNext()
    {
        GameObject nextCreep = null;

        if(amountToSpawn[index] <= 0)
        {
            index++;
        }

        if(index < spawnedCreeps.Length)
        {
            nextCreep = spawnedCreeps[index];
            amountToSpawn[index]--;
        }

        return nextCreep;
    }
}
