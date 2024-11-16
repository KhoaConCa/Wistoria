using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
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
        var keys = new List<GameObject>(_objectState.Keys);

        foreach (var key in keys)
        {
            _objectState[key] = false;
        }

        if (_objectState.ContainsKey(targetObject))
        {
            _objectState[targetObject] = true;
        }
        else
        {
            Debug.LogWarning("GameObject is not included in Dictionary");
        }

        UpdateObjectUI();
    }

    /// <summary>
    /// Set Active to selected UI
    /// </summary>
    /// <param name="targetTag">Tag's GameObject need to show</param>
    public void SetActiveObjectUI(string targetTag)
    {
        try
        {
            if (_objectUIs.Count <= 0) SetupDictionary();

            GameObject targetObject = null;

            foreach (var item in _objectUIs)
            {
                if (item.tag == targetTag)
                {
                    targetObject = item;
                    break;
                }
            }

            var keys = new List<GameObject>(_objectState.Keys);

            foreach (var key in keys)
                _objectState[key] = false;

            if (_objectState.ContainsKey(targetObject))
                _objectState[targetObject] = true;

            UpdateObjectUI();
        }
        catch (Exception e)
        {
            Debug.LogWarning($"Error in: {e.Message}");
        }

    }

    #endregion

    #region -- Methods --

    void Start()
    {
        SetupDictionary();
        SetActiveObjectUI(_defaultUI);
    }

    /// <summary>
    /// Show GameObject
    /// </summary>
    private void UpdateObjectUI()
    {
        try
        {
            foreach (var entry in _objectState)
            {
                entry.Key.SetActive(entry.Value);
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning(e.Message);
        }
    }

    /// <summary>
    /// Find children object in this object
    /// </summary>
    private void SetupDictionary()
    {
        try
        {
            _objectState.Clear();

            if (this.gameObject.transform == null)
            {
                Debug.LogWarning("object transform is not assigned. Please assign it in the Inspector.");
                return;
            }

            foreach (Transform child in this.gameObject.transform)
            {
                _objectState[child.gameObject] = false;
                _objectUIs.Add(child.gameObject);
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning(e.Message);
        }

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
            if (item.tag == tagTarget)
                return item;
        }

        return null;
    }

    #endregion

    #region -- Properties --



    #endregion

    #region -- Fields --

    [SerializeField] private Transform _objectTransform;

    [TagSelector] [SerializeField] private string _tagName;
    [TagSelector] [SerializeField] private string _defaultUI;

    [SerializeField] private List<GameObject> _objectUIs = new List<GameObject>();
    
    private Dictionary<GameObject, bool> _objectState = new Dictionary<GameObject, bool>();

    #endregion
}
