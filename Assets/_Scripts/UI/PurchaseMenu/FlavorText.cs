using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class FlavorText : MonoBehaviour
{
    public GameObject tooltipPanel;

    public Text heroName;
    public Text cost;
    public Text damage;
    public Text speed;
    public Text description;

    public void Start()
    {
        ClearText();
    }

    public void ClearText()
    {
        tooltipPanel.SetActive(false);
    }

    public void SetGenjiText()
    {
        tooltipPanel.SetActive(true);

        heroName.text    = "GENJI";
        cost.text = "100";
        damage.text = "High";
        speed.text = "Medium";
        description.text =
            "Throws three throwing stars in quick succession.";
    }

    public void SetPharahText()
    {
        tooltipPanel.SetActive(true);

        heroName.text = "PHARAH";
        cost.text = "150";
        damage.text = "Medium";
        speed.text = "Slow";
        description.text =
            "Fires a rocket that deals damage to all nearby enemies.";
    }

    public void SetMeiText()
    {
        tooltipPanel.SetActive(true);

        heroName.text = "MEI";
        cost.text = "300";
        damage.text = "Low";
        speed.text = "Fast";
        description.text =
            "Fires a continuous stream of frost that slows enemies.";
    }

    public void SetLucioText()
    {
        tooltipPanel.SetActive(true);

        heroName.text = "LUCIO";
        cost.text = "250";
        damage.text = "None";
        speed.text = "N/A";
        description.text =
            "Heals nearby allies but does not attack.";
    }

}
