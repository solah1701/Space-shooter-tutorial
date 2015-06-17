using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HighScoreInput : MonoBehaviour
{
    public InputField inputField;
    private bool visible;
    private Image image;
    private InputField text;

    void Awake()
    {
        visible = false;
        image = GetComponent<Image>();
        if (image == null) throw new MissingComponentException("Image script is missing");
        text = GetComponent<InputField>();
        if (text == null) throw new MissingComponentException("Text script is missing");
        Show();
    }

    public void ToggleShow()
    {
        visible = !visible;
        Show();
    }

    private void Show()
    {
        image.enabled = visible;
        text.textComponent.enabled = visible;
        if (visible) inputField.ActivateInputField();
        else inputField.DeactivateInputField();
    }
}
