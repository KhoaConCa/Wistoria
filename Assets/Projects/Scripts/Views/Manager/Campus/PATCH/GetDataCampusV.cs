using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Utilities;

public class GetDataCampusV : MonoBehaviour, ICampusDataGetter
{
    #region -- Implements --

    /// <summary>
    /// Set data for CampusD from Textbox
    /// </summary>
    /// <param name="campus">Campus data</param>
    public void GetCampusData(CampusD campus)
    {
        campus.CampusName = campusName.text;
        campus.Room = campusRoom.text;
    }

    #endregion

    #region -- Methods --

    void Start()
    {
        AddTextComponent();
    }

    private void AddTextComponent()
    {
        Transform nameTransform = transform.Find(_campusName);

        if (nameTransform != null)
        {
            campusName = nameTransform.GetComponent<TextMeshProUGUI>();
        }

        Transform roomTransform = transform.Find(_campusRoom);

        if (roomTransform != null)
        {
            campusRoom = roomTransform.GetComponent<TextMeshProUGUI>();
        }
    }

    #endregion

    #region -- Fields --


    public TextMeshProUGUI campusName;
    public TextMeshProUGUI campusRoom;

    private readonly string _campusName = "Campus/Value";
    private readonly string _campusRoom = "Room/Value";

    #endregion
}
