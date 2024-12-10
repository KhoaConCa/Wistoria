using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITagV : MonoBehaviour, IPrinterTagV
{
    #region -- Implements --

    public void SetUpQueue(int slotRemining)
    {
        switch(slotRemining)
        {
            case 0:

                // Đầy
                ColorPalette.Semantics.TryGetValue(3, out string full);
                if (ColorUtility.TryParseHtmlString(full, out Color fullColor))
                {
                    _badge.GetComponent<Image>().color = fullColor;
                    _slotText.color = fullColor;

                    _slotText.text = "Đầy";
                }

                break;

            case 3:

                // Trống
                ColorPalette.Semantics.TryGetValue(2, out string empty);
                if (ColorUtility.TryParseHtmlString(empty, out Color emptyColor))
                {
                    _badge.GetComponent<Image>().color = emptyColor;
                    _slotText.color = emptyColor;

                    _slotText.text = "Trống";
                }

                break;

            default:

                // Gần đầy
                ColorPalette.Semantics.TryGetValue(1, out string almostFull);
                if (ColorUtility.TryParseHtmlString(almostFull, out Color almostFullColor))
                {
                    _badge.GetComponent<Image>().color = almostFullColor;
                    _slotText.color = almostFullColor;

                    _slotText.text = "Gần đầy";
                }

                break;
        }
    }

    #endregion

    #region -- Methods --

    private void Awake()
    {
        GetComponent();
    }

    #region -- Get Component --
    private void GetComponent()
    {
        if (_tagGroup == null)
            _tagGroup = this.gameObject.GetComponentInParent<UITagGroupV>();
    }
    #endregion

    #region -- Selected Tag Event --
    public void SelectedTag()
    {
        _tagGroup.OnSelected(this.gameObject);
    }
    #endregion

    #endregion

    #region -- Fields --

    private ITagGroupV _tagGroup;

    [SerializeField] private GameObject _badge;
    [SerializeField] private TextMeshProUGUI _slotText;

    #endregion
}
