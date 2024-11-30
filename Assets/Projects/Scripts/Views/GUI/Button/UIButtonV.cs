using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

public class UIButtonV : MonoBehaviour, IButtonV
{
    #region -- Implements --

    public void OnSelected()
    {
        try
        {
            _group.ResetButton(this.gameObject);
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }

        foreach (Shadow shadow in _shadows)
            shadow.enabled = true;

        if (ColorUtility.TryParseHtmlString("#005CB3", out Color color))
            _background.color = color;
    }

    public void OnExited()
    {
        foreach (Shadow shadow in _shadows)
            shadow.enabled = false;

        if (ColorUtility.TryParseHtmlString("#57C4FF", out Color color))
            _background.color = color;
    }
    #endregion

    #region -- Methods --

    private void Awake()
    {
        GetComponent();
    }

    private void GetComponent()
    {
        GameObject footer = GameObject.FindWithTag(_tagParent);
        _group = footer.GetComponent<UIButtonGroup>();

        _shadows = this.gameObject.GetComponents<Shadow>().ToList();
        _background = this.gameObject.GetComponent<Image>();
    }

    #endregion

    #region -- Fields --

    private IButtonGroupV _group;

    [SerializeField] private List<Shadow> _shadows = new List<Shadow>();
    [SerializeField] private Image _background = null;

    [SerializeField] private string _tagParent = "Footer";

    #endregion
}
