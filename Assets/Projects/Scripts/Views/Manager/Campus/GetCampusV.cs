using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetCampusV : MonoBehaviour, ICampusView
{
    #region -- Methods --

    /*    public void DisplayCampus(List<CampusD> campuses)
        {
            if (campuses.Count > 0)
            {
                var campus = campuses[0];
                campusName.text = campus.CampusName;
                campusRoom.text = campus.Room;
            }
        }*/

    public void SetCampusData(CampusD campus)
    {
        campusName.text = campus.CampusName;
        campusRoom.text = campus.Room;
    }

    #endregion

    #region -- Fields --

    public TextMeshProUGUI campusName;
    public TextMeshProUGUI campusRoom;

    #endregion
}
