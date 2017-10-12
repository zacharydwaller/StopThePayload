using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour
{
    public GameObject parentObj;
    public Image fill;
    public Image background;

    public void Start()
    {
        fill = transform.FindChild("Fill Area").GetComponentInChildren<Image>();
        background = transform.FindChild("Background").GetComponent<Image>();
    }

    public void Update()
    {
        if(!parentObj)
        {
            Destroy(gameObject);
        }
    }
}
