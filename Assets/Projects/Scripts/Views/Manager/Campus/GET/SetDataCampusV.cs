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
        campusName = nameLocation.GetComponent<TextMeshProUGUI>();
        campusRoom = roomLocation.GetComponent<TextMeshProUGUI>();
    }

    public void SetCampusName(string name)
    {
        campusName.text = name;
    }

    public void SetCampusRoom(string room)
    {
        campusRoom.text = room;
    }

    #endregion

    #region -- Methods --

    #endregion

    #region -- Fields --

    public TextMeshProUGUI campusName;
    public TextMeshProUGUI campusRoom;

    #endregion
}
