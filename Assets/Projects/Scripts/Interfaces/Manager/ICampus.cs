using Newtonsoft.Json.Bson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region -- Handlers --

#region -- Get Campus --
public interface IGetCampusHandler
{
    IEnumerator GetCampus(string campusName, Action<CampusD> onCampusFound);
}
#endregion

#region -- Modify Campus --
public interface IModifyCampusHandler
{
    IEnumerator CampusInformation(CampusD campus, Action<CampusD> onCampusFound);
}
#endregion

#endregion

#region -- View --

#region -- Get Campus --
public interface ISpawnCampusView
{
    void CreateCard(CampusD campus);
}

public interface ISetDataCampusView
{
    void SetCampusName(string name);
    void SetCampusRoom(string room);
    void AddComponentFromPrefab(Transform nameLocation, Transform roomLocation);
}
#endregion

#region -- Modify Campus --
public interface IModifyCampusView
{

}
#endregion

#endregion