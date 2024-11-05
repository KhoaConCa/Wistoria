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

    void Awake()
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
        foreach(var campusUI in campusUIs)
        {
            _campusState[campusUI] = false;
        }
    }

    #endregion

    #region -- Fields --

    [SerializeField] private List<GameObject> campusUIs = new List<GameObject>();

    private Dictionary<GameObject, bool> _campusState = new Dictionary<GameObject, bool>();

    #endregion
}
