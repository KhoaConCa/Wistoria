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
    void SetupButton();
    void GetCampusID(string id);
}
#endregion

public interface ICampusDetail
{
    void DisplayCampusDetails(ICampusCardData cardData);
}

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

public interface IDetailUpdate
{

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
    IEnumerator UpdateCampusData(CampusD campus, Action<CampusD> onCampusUpdated);
}
#endregion

#endregion

#region -- View --

public interface ICampusComponentAdder
{
    void AddComponentFromPrefab(Transform nameLocation, Transform roomLocation);
}

//
public interface ICampusCardClicker
{
    void Initialize(string campusID, Action<string> onClickCallback);
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
    void GetCampusID(CampusD campus);
    void SetCampusID(string id);
}

public interface ITransformUI
{
    void SetActiveCampusUI(GameObject targetCampus);
}

public interface ISetDataCampusInfo
{
    void SetCampusNameInput(string name);
    void SetCampusRoomInput(string room);
}
#endregion

public interface ICampusCardData
{
    string CampusID { get; set; }
    string CampusName { get; set; }
    string CampusRoom { get; set; }
    void Initialize(string id, string name, string room);
}
#endregion