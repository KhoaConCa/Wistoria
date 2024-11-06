using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITransformV : MonoBehaviour, ITransformUI
{
    #region -- Implements --

    /// <summary>
    /// Set Active to selected UI
    /// </summary>
    /// <param name="targetCampus">GameObject need to show</param>
    public void SetActiveCampusUI(GameObject targetCampus)
    {
        var keys = new List<GameObject>(_campusState.Keys);

        foreach (var key in keys)
        {
            _campusState[key] = false;
        }

        if (_campusState.ContainsKey(targetCampus))
        {
            _campusState[targetCampus] = true;
        }
        else
        {
            Debug.LogWarning("GameObject is not included in Dictionary");
        }

        UpdateCampusUI();
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
    private void UpdateCampusUI()
    {
        foreach (var entry in _campusState)
        {
            entry.Key.SetActive(entry.Value);
        }
    }

    private void SetupDictionary()
    {
        _campusState.Clear();

        FindParentTransform();

        if (campusTransform == null)
        {
            Debug.LogWarning("Campus transform is not assigned. Please assign it in the Inspector.");
            return;
        }

        foreach (Transform child in campusTransform)
        {
            _campusState[child.gameObject] = false;
            campusUIs.Add(child.gameObject);
        }

        Debug.Log("Dictionary setup completed with all child GameObjects in DetailCampus.");
    }

    private void FindParentTransform()
    {
        if (campusTransform == null)
        {
            GameObject foundCampus = GameObject.Find("Campus");

            if (foundCampus != null)
            {
                campusTransform = foundCampus.transform;
            }
            else
            {
                Debug.LogWarning("DetailCampus GameObject not found in the scene. Please check the name.");
            }
        }
    }

    #endregion

    #region -- Fields --

    [SerializeField] private Transform campusTransform;
    [SerializeField] private List<GameObject> campusUIs = new List<GameObject>();

    private Dictionary<GameObject, bool> _campusState = new Dictionary<GameObject, bool>();

    #endregion
}
