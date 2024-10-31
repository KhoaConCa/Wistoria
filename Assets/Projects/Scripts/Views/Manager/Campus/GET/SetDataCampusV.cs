using System.Collections;
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

    #endregion

    #region -- Methods --

    void Start()
    {
        AddComponentModify();
        //_modifyCampus.GetCampusData(campusName.text, campusRoom.text);
    }

    private void AddComponentModify()
    {
        if (_modifyCampus == null)
        {
            _modifyCampus = gameObject.AddComponent<ModifyCampusV>();
        }
        else
        {
            Debug.Log("Đã tồn tại component ModifyCampusV");
        }
    }

    #endregion

    #region -- Fields --

    public TextMeshProUGUI campusName;
    public TextMeshProUGUI campusRoom;

    private IModifyCampusView _modifyCampus;

    #endregion
}
