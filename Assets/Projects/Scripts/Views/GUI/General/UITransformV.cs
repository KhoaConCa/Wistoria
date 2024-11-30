using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SearchService;

public class UITransformV : MonoBehaviour, ITransformUI
{
    #region -- Implements --

    /// <summary>
    /// Set Active to selected UI
    /// </summary>
    /// <param name="targetobject">GameObject need to show</param>
    public void SetActiveObjectUI(GameObject targetObject)
    {
        foreach (GameObject item in _objectUIs)
        {
            if (item.name == targetObject.name)
                item.SetActive(true);
            else
                item.SetActive(false);
        }
    }

    #endregion

    #region -- Methods --

    void OnEnable()
    {
        SetActiveObjectUI(_defaultUI);
    }

    /// <summary>
    /// Find target object in list GameObject by tag.
    /// </summary>
    /// <param name="tagTarget">Fill in tag target</param>
    /// <returns>GameObject needed</returns>
    public GameObject FindTargetObjectByTag(string tagTarget)
    {
        foreach(var item in _objectUIs)
        {
            if (item.name == tagTarget)
                return item;
        }

        return null;
    }

    #endregion

    #region -- Fields --

    [SerializeField] private GameObject _defaultUI;

    [SerializeField] private List<GameObject> _objectUIs = new List<GameObject>();

    #endregion
}
