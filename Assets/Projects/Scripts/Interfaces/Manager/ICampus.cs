using Newtonsoft.Json.Bson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region -- Commands --

#region -- Get Campus --
public interface IGetCampusCommand
{
    void ClickFindButton();
}
#endregion

#region -- Modify Campus --
public interface IModifyCampusCommand
{
    void ClickCard();
    void SetupButton(GameObject prefab);
}
#endregion

#endregion

#region -- Handlers --

#region -- Get Campus --
public interface IGetCampusHandler
{
    IEnumerator GetAllCampus(Action<CampusD> onCampusFound);
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

public interface ICampusComponentAdder
{
    void AddComponentFromPrefab(Transform nameLocation, Transform roomLocation);
}

#region -- Get Campus --
public interface ICampusViewSpawner
{
    void CreateCard(CampusD campus);
}

public interface ICampusDataSetter : ICampusComponentAdder
{
    void SetCampusName(string name);
    void SetCampusRoom(string room);
}
#endregion

#region -- Modify Campus --
public interface ICampusDataGetter
{
    void GetCampusData(CampusD campus);
}
#endregion

#endregion