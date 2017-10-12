using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    protected GameManager gameManager;
    protected Collider area;

    public void Start()
    {
        area = GetComponent<Collider>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Payload"))
        {
            gameManager.SendMessage("CheckpointReached");
        }
    }
}
