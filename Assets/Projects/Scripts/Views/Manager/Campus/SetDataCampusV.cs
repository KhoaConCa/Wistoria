using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


public class SetDataCampusV : MonoBehaviour, ISetDataCampusView
{
    #region -- Implements --

    public void Initialization()
    {

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

    void Start()
    {
        Initialization();
    }

    #endregion

    #region -- Fields --

    public TextMeshProUGUI campusName;
    public TextMeshProUGUI campusRoom;

    #endregion
}
