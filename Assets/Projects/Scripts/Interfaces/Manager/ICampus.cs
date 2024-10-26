using Newtonsoft.Json.Bson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region -- Commands --

public interface ICampusCommand
{
    void Initialization();
}

public interface IGetCampusCommand : ICampusCommand
{
    void ClickFindButton();
}

#endregion

#region -- Handlers --

public interface ICampusHandler
{

}

public interface IGetCampusHandler : ICampusHandler
{
    IEnumerator GetCampus(string campusName, Action<CampusD> onCampusFound);
}

public interface ICreateCampusHandler : ICampusHandler
{

}

#endregion

#region -- View --

public interface ICampusView
{
    void Initialization();
}

public interface ISpawnCampusView : ICampusView
{
    void CreateCard(CampusD campus);
}

public interface ISetDataCampusView : ICampusView
{
    void SetCampusName(string name);
    void SetCampusRoom(string room);
    void AddComponentFromPrefab(Transform nameLocation, Transform roomLocation);
}

#endregion