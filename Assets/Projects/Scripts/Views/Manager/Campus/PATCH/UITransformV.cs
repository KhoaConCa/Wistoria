using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampusUIManager : MonoBehaviour, ITransformUI
{
    #region -- Implements --

    /// <summary>
    /// Show GameObject
    /// </summary>
    public void UpdateCampusUI()
    {
        foreach (var entry in _campusState)
        {
            entry.Key.SetActive(entry.Value);
        }
    }

    #endregion

    #region -- Methods --

    void Awake()
    {
        _campusState[createCampusUI] = false;
        _campusState[detailCampusUI] = false;
        _campusState[findCampusUI] = false;
    }

    /// <summary>
    /// Set Active to selected UI
    /// </summary>
    /// <param name="targetCampus">GameObject need to show</param>
    private void SetActiveCampusUI(GameObject targetCampus)
    {
        foreach (var key in _campusState.Keys)
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

    #region -- Fields --
    [SerializeField] private GameObject createCampusUI;
    [SerializeField] private GameObject detailCampusUI;
    [SerializeField] private GameObject findCampusUI;

    private Dictionary<GameObject, bool> _campusState = new Dictionary<GameObject, bool>();
    #endregion
}
