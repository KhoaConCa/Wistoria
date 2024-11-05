using Newtonsoft.Json.Bson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
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

#region -- Add Campus --
public interface IAddCampusCommand
{
    void ClickAddButton();
}
#endregion

#endregion

#region -- Handlers --

public interface IDataTransfer
{
    void TransferData(string response);
}

#region -- Get Campus --
public interface IGetCampusHandler : IDataTransfer
{
    IEnumerator GetAllCampus(Action<CampusD> onCampusFound);
    IEnumerator GetCampus(string campusName, Action<CampusD> onCampusFound);
}
#endregion

#region -- Modify Campus --
public interface IModifyCampusHandler : IDataTransfer
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

public interface ITransformUI
{
    void SetActiveCampusUI(GameObject targetCampus);
}
#endregion

#endregion