using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HeroButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public FlavorText flavorText;

    public string heroName;
    public GameObject heroRef;

    protected PlayerController playerController;

    public void Start()
    {
        playerController =
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    public void OnClick()
    {
        playerController.HoldHero(heroRef);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        playerController.heroInValidPos = false;

        if(heroName.Equals("Genji"))
        {
            flavorText.SetGenjiText();
        }
        else if(heroName.Equals("Pharah"))
        {
            flavorText.SetPharahText();
        }
        else if(heroName.Equals("Mei"))
        {
            flavorText.SetMeiText();
        }
        else if(heroName.Equals("Lucio"))
        {
            flavorText.SetLucioText();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        playerController.heroInValidPos = true;

        flavorText.ClearText();
    }
}
