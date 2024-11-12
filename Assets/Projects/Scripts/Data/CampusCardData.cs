using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CampusCardData : MonoBehaviour, ICampusCardData
{
    #region -- Implements --

    public void Initialize(string id, string name, string room)
    {
        CampusID = id;
        CampusName = name;
        CampusRoom = room;
    }

    #region -- Properties --
    public string CampusID { get; set; }
    public string CampusName { get; set; }
    public string CampusRoom { get; set; }
    #endregion

    #endregion

    #region -- Methods --

    #endregion

    #region -- Fields --

    public Button clickButton;

    #endregion
}
