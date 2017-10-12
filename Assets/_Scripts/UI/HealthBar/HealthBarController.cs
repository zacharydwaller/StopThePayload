using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBarController : MonoBehaviour
{
    public GameObject healthBarRef;
    public float yOffset;

    public bool overrideValue = false;

    protected GameObject healthBarObj;
    protected Slider healthBarUI;
    protected HealthBar healthBarScript;

    protected GameObject parentObj;
    protected Entity parent;

    public void Start()
    {
        parentObj = transform.parent.gameObject;
        parent = GetComponentInParent<Entity>();

        healthBarObj = Instantiate(healthBarRef);
        healthBarObj.transform.SetParent(GameObject.FindGameObjectWithTag("EntityUI").transform);
        healthBarObj.GetComponent<HealthBar>().parentObj = parentObj;

        healthBarUI = healthBarObj.GetComponent<Slider>();
        healthBarUI.minValue = 0f;
        healthBarUI.maxValue = parent.maxHealth;

        healthBarScript = healthBarObj.GetComponent<HealthBar>();
    }

    public void Update()
    {
        if(!overrideValue)
        {
            UpdateHealth();
        }
        
        UpdatePos();
    }

    public void SetColor(Color newColor)
    {
        newColor.a = 175;
        healthBarScript.fill.color = newColor;
    }

    public void SetValue(float newValue)
    {
        healthBarUI.value = newValue;
    }

    public void SetMaxValue(float newValue)
    {
        healthBarUI.maxValue = newValue;
    }

    public void UpdatePos()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(parentObj.transform.position);
        screenPos.y += yOffset;

        healthBarUI.transform.position = screenPos;
    }

    public void UpdateHealth()
    {
        if(parent.health == parent.maxHealth)
        {
            healthBarObj.SetActive(false);
        }
        else
        {
            healthBarObj.SetActive(true);
            healthBarUI.value = parent.health;
        }
    }
}