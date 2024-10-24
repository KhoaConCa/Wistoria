using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiCampus : MonoBehaviour
{
    #region -- Methods --

    void Start()
    {
        var campusHandler = gameObject.AddComponent<GetCampusH>();

        //campusCommand.Init(campusHandler, campusView);
    }

    #endregion 

    #region -- Fields --

    public GetCampusC campusCommand;
    public GetCampusV campusView;

    #endregion
}
