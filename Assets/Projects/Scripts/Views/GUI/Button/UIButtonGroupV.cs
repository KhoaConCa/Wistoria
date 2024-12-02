using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using UnityEditor;
using UnityEngine;

public class UIButtonGroup : MonoBehaviour, IButtonGroupV
{
    #region -- Implements --

    public void ResetButton(GameObject currentObject)
    {
        if (currentObject == null)
        {
            Debug.LogError("Gameobject can't be found!");
            return;
        }

        _button.OnExited();
        _buttonDefault = currentObject;
        GetComponent();
    }

    #endregion

    #region -- Methods --

    private void Start()
    {
        GetComponent();
        SetDefaultButton();
    }

    private void GetComponent()
    {
        if (_buttonDefault != null)
        {
            _button = _buttonDefault.GetComponent<UIButtonV>();
        }
    }

    private void SetDefaultButton()
    {
        _button.OnSelected();
    }

    #endregion

    #region -- Fields --

    private IButtonV _button;

    [SerializeField] private GameObject _buttonDefault;

    #endregion
}
