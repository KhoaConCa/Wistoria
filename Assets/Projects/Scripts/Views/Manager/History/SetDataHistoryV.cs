using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


public class SetDataHistoryV : MonoBehaviour, IHistoryDataSetter
{
    #region -- Implements --

    /// <summary>
    /// Add componet to from prefab selected
    /// </summary>
    /// <param name="valueLocation">Location of Text value Field</param>
    public void AddComponentFromPrefab(Transform valueLocation)
    {
        _historyValue = valueLocation.GetComponent<TextMeshProUGUI>();
    }

    public void SetHistoryData(string value)
    {
        _historyValue.text = value;
    }

    #endregion

    #region -- Fields --

    private TextMeshProUGUI _historyValue;

    #endregion
}
