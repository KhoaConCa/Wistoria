using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITextBoxV : MonoBehaviour, ITextBoxHandler
{
    #region -- Implements --

    public void OnError(string message)
    {
        string errorColor = "";
        ColorPalette.Semantics.TryGetValue(3, out errorColor);
        if (ColorUtility.TryParseHtmlString(errorColor, out Color color))
        {
            _textHolder.color = color;
            _placeHolder.color = color;
            _outline.effectColor = color;
        }

        _textHelper.SetActive(true);
        TextMeshProUGUI textValue = _textHelper.GetComponent<TextMeshProUGUI>();
        textValue.color = color;
        textValue.text = message;
    }

    #endregion

    #region -- Methods --

    public void OnValueChange(string message)
    {
        string selectedColor = "";
        ColorPalette.Semantics.TryGetValue(0, out selectedColor);
        if (ColorUtility.TryParseHtmlString(selectedColor, out Color color))
            _outline.effectColor = color;

        if (!string.IsNullOrEmpty(message))
            DefaultTextHelper(message);
        else
            _textHelper.SetActive(false);
    }

    public void OnEnterEdit(string message)
    {
        string defaultColor = "";
        ColorPalette.Neutral.TryGetValue(3, out defaultColor);
        if (ColorUtility.TryParseHtmlString(defaultColor, out Color color))
        {
            _textHolder.color = color;
            _placeHolder.color = color;
            _outline.effectColor = color;
        }

        if (!string.IsNullOrEmpty(message))
            DefaultTextHelper(message);
        else
            _textHelper.SetActive(false);
    }

    public void OnEndEdit(string message)
    {
        string defaultColor = "";
        ColorPalette.Neutral.TryGetValue(3, out defaultColor);
        if (ColorUtility.TryParseHtmlString(defaultColor, out Color color))
        {
            _textHolder.color = color;
            _placeHolder.color = color;
            _outline.effectColor = color;
        }

        if (!string.IsNullOrEmpty(message))
            DefaultTextHelper(message);
        else
            _textHelper.SetActive(false);
    }

    private void DefaultTextHelper(string message)
    {
        _textHelper.SetActive(true);

        TextMeshProUGUI helper = _textHelper.GetComponent<TextMeshProUGUI>();

        ColorPalette.Neutral.TryGetValue(2, out string defaultColor);
        if (ColorUtility.TryParseHtmlString(defaultColor, out Color color))
            helper.color = color;

        helper.text = message;
    }

    #endregion

    #region -- Fields --

    [SerializeField] private TextMeshProUGUI _textHolder;
    [SerializeField] private TextMeshProUGUI _placeHolder;

    [SerializeField] private GameObject _textHelper;

    [SerializeField] private Outline _outline;

    #endregion
}
