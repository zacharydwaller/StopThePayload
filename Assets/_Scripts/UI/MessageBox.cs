using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageBox : MonoBehaviour
{
    public Text messageText;
    public Image messagePanel;

    protected float panelFullAlpha;

    public float messageDuration;

    public AnimationCurve fadeCurve;

    protected float beginFadeTime;

    public void Start()
    {
        beginFadeTime = -Mathf.Infinity;
        panelFullAlpha = messagePanel.color.a;
    }

    public void Update()
    {
        float newAlpha;
        Color newColor;

        newAlpha = fadeCurve.Evaluate(Time.time - beginFadeTime);

        newColor = messagePanel.color;
        newColor.a = newAlpha * panelFullAlpha;
        messagePanel.color = newColor;

        newColor = messageText.color;
        newColor.a = newAlpha;
        messageText.color = newColor;
    }

    public void SetMessage(string message)
    {
        messageText.text = message;

        beginFadeTime = Time.time + messageDuration;
    }
}
