using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetInfoCampusV : MonoBehaviour, ISetDataCampusInfo
{
    #region -- Implements --

    public void SetCampusNameInput(string name)
    {
        nameField.text = name;
    }

    public void SetCampusRoomInput(string room)
    {
        roomField.text = room;
    }

    #endregion

    #region -- Methods --
    #endregion

    #region -- Fields --
    public TextMeshProUGUI nameField;
    public TextMeshProUGUI roomField;
    #endregion
}
