using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public int snapValue;
    public int money;
    public int lives;

    protected GameObject heldHero;
    [HideInInspector]
    public bool heroInValidPos = false;

    public void Start()
    {
        
    }

    public void Update()
    {
        if(heldHero)
        {
            DragHero();

            if(Input.GetMouseButtonDown(0) && heroInValidPos)
            {
                PlaceHero();
            }
            else if(Input.GetMouseButtonDown(1))
            {
                DeleteHero();
            }
        }
        
    }

    public void LoseLife()
    {
        lives--;

        if(lives <= 0)
        {
            GameObject.FindGameObjectWithTag("GameManager").SendMessage("GameOver");
        }
    }

    public void HoldHero(GameObject hero, bool destroyCurrent = true)
    {
        if(destroyCurrent && heldHero != null)
        {
            Destroy(heldHero);
        }

        heldHero = Instantiate(hero, Vector3.zero - Vector3.up * 1000f, Quaternion.identity);
    }

    public void DeleteHero()
    {
        Destroy(heldHero);
        heldHero = null;
    }

    public void PlaceHero()
    {
        GameObject placedHero;
        int cost = heldHero.GetComponent<Tower>().cost;

        if(money >= cost)
        {
            money -= cost;

            placedHero = heldHero;

            if(Input.GetKey(KeyCode.LeftShift))
            {
                HoldHero(heldHero, false);
            }
            else
            {
                heldHero = null;
            }

            placedHero.GetComponent<Tower>().Place();
            placedHero.layer = LayerMask.NameToLayer("Default");
        }
        
    }

    public void DragHero()
    {
        Ray ray;
        RaycastHit rayHit;
        Vector3 position;

        heldHero.layer = LayerMask.NameToLayer("Ignore Raycast");

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out rayHit))
        {
            if(rayHit.transform.tag.Equals("TowerZone") && rayHit.normal == Vector3.up)
            {
                position = rayHit.point;
                heldHero.transform.position = position;

                AutoSnap();
            }
        }
        
    }

    public void AutoSnap()
    {
        Vector3 position = heldHero.transform.position;

        position.x = Round(position.x);
        position.y += 1.0f;
        position.z = Round(position.z);

        heldHero.transform.position = position;
    }

    private float Round(float input)
    {
        return snapValue * Mathf.Round((input / snapValue));
    }
}
