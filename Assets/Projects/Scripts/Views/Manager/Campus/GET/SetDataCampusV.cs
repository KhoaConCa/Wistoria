﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


public class SetDataCampusV : MonoBehaviour, ISetDataCampusView
{
    #region -- Implements --

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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="nameLocation"></param>
    /// <param name="roomLocation"></param>
    public void AddComponentFromPrefab(Transform nameLocation, Transform roomLocation)
    {
        campusName = nameLocation.GetComponent<TextMeshProUGUI>();
        campusRoom = roomLocation.GetComponent<TextMeshProUGUI>();
    }

    #endregion

    #region -- Fields --

    public TextMeshProUGUI campusName;
    public TextMeshProUGUI campusRoom;

    #endregion
}
