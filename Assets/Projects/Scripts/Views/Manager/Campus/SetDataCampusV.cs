using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


public class SetDataCampusV : MonoBehaviour, ICampusDataSetter
{
    #region -- Implements --

    /// <summary>
    /// Add componet to from prefab selected
    /// </summary>
    /// <param name="nameLocation">Location of Text Name Field</param>
    /// <param name="roomLocation">Location of Text Room Field</param>
    public void AddComponentFromPrefab(Transform nameLocation, Transform roomLocation)
    {
        _campusName = nameLocation.GetComponent<TextMeshProUGUI>();
        _campusRoom = roomLocation.GetComponent<TextMeshProUGUI>();
    }

    public void SetCampusData(ICampusCardData campus)
    {
        _campusName.text = campus.Name;
        _campusRoom.text = campus.Room;
    }

    #endregion

    #region -- Fields --

    private TextMeshProUGUI _campusName;
    private TextMeshProUGUI _campusRoom;

    #endregion
}
