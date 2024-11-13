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
    }

    /// <summary>
    /// Show GameObject
    /// </summary>
    private void UpdateObjectUI()
    {
        foreach (var entry in _objectState)
        {
            entry.Key.SetActive(entry.Value);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void SetupDictionary()
    {
        _objectState.Clear();

        FindParentTransform();

        if (_objectTransform.transform == null)
        {
            Debug.LogWarning("object transform is not assigned. Please assign it in the Inspector.");
            return;
        }

        foreach (Transform child in _objectTransform.transform)
        {
            _objectState[child.gameObject] = false;
            _objectUIs.Add(child.gameObject);
        }

        Debug.Log("Dictionary setup completed with all child GameObjects in Detailobject.");
    }


    /// <summary>
    /// 
    /// </summary>
    private void FindParentTransform()
    {
        if (_objectTransform == null)
        {
            GameObject foundobject = GameObject.FindGameObjectWithTag(_tagName);

            if (foundobject != null)
            {
                _objectTransform = foundobject.transform;
            }
            else
            {
                Debug.LogWarning("Detail gameObject not found in the scene. Please check the name.");
            }
        }
    }

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

    [SerializeField] private List<GameObject> _objectUIs = new List<GameObject>();
    
    private Dictionary<GameObject, bool> _objectState = new Dictionary<GameObject, bool>();

    #endregion
}
