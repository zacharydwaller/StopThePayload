using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Color selectedColor;
    public Color normalColor;

    public float selectedSize;
    public float normalSize;

    public float xOffset;

    protected Text text;
    protected RectTransform rectTransf;

    public void Start()
    {
        text = GetComponentInChildren<Text>();
        rectTransf = text.GetComponent<RectTransform>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        rectTransf.sizeDelta = new Vector2(
            rectTransf.rect.width,
            selectedSize);

        rectTransf.anchoredPosition = new Vector2(
            rectTransf.anchoredPosition.x + xOffset,
            rectTransf.anchoredPosition.y
            );

        text.color = selectedColor;

        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        rectTransf.sizeDelta = new Vector2(
            rectTransf.rect.width,
            normalSize);

        rectTransf.anchoredPosition = new Vector2(
            rectTransf.anchoredPosition.x - xOffset,
            rectTransf.anchoredPosition.y
            );

        text.color = normalColor;
    }
}
