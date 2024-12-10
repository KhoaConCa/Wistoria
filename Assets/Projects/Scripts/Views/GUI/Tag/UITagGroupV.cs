using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITagGroupV : MonoBehaviour, ITagGroupV
{
    #region -- Implements --

    public void OnSelected(GameObject tag)
    {
        if (_currentTag == null)
        {
            _currentTag = tag;

            ColorPalette.Neutral.TryGetValue(1, out string selected);
            if (ColorUtility.TryParseHtmlString(selected, out Color color))
                _currentTag.GetComponent<Image>().color = color;

            return;
        }

        if (_currentTag != null && _currentTag.name != tag.name)
        {
            OnReset();

            _currentTag = tag;

            ColorPalette.Neutral.TryGetValue(1, out string selected);
            if (ColorUtility.TryParseHtmlString(selected, out Color color))
                _currentTag.GetComponent<Image>().color = color;

            return;
        }


        OnReset();
        _currentTag = null;
    }
    public void OnReset()
    {
        ColorPalette.Neutral.TryGetValue(0, out string reset);
        if (ColorUtility.TryParseHtmlString(reset, out Color color))
            _currentTag.GetComponent<Image>().color = color;
    }

    #endregion

    #region -- Fields --

    [SerializeField] private GameObject _currentTag;

    #endregion
}
