using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationV : MonoBehaviour, IGUI
{
    #region -- Implements --

    public void ClickUIOnEvent(GameObject mainScene)
    {
        try
        {
            SwitchUI(ref mainScene);
        }
        catch (Exception e)
        {
            Debug.Log($"Error in switch UI: {e.Message}");
        }
    }

    #endregion

    #region -- Methods --

    private void Start()
    {
        SetAsDefault();
    }

    private void SetAsDefault()
    {
        if (_mainSceneCurrent != null)
        {
            if (!_mainSceneCurrent.activeSelf) 
                _mainSceneCurrent.SetActive(true);

            return;
        }

        _mainSceneCurrent = GameObject.FindGameObjectWithTag("DefaultMainScene");
    }

    private void SwitchUI(ref GameObject mainScene)
    {
        _mainSceneCurrent.SetActive(false);
        _mainSceneCurrent = mainScene;
        _mainSceneCurrent.SetActive(true);
    }

    #endregion

    #region -- Fields --

    [SerializeField]
    private GameObject _mainSceneCurrent;

    #endregion
}
