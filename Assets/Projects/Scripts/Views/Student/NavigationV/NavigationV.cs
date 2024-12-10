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
        try
        {
            SwitchUI(ref _defaultUI);
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    private void SwitchUI(ref GameObject mainScene)
    {
        foreach (var item in _functionUIs)
        {
            if (item.name == mainScene.name)
                item.SetActive(true);
            else
                item.SetActive(false);
        }
    }

    #endregion

    #region -- Fields --

    [SerializeField] private GameObject _defaultUI;

    [SerializeField] private List<GameObject> _functionUIs;

    #endregion
}
