using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICampus
{
    public IEnumerator GetUniqueName(Action<List<string>> onNameCampus, Action<string> onSuccess, Action<string> onFailed);
}

public interface IDetailCampusUpdateHandler : ICampus
{
    IEnumerator UpdateCampusData(CampusD campus, Action<string> onSuccess, Action<string> onFailed);
    IEnumerator GetUniqueRoom(Action<List<string>> onRoomCampus, Action<string> onSuccess, Action<string> onFailed);
}

public interface IGetCampusHandler : ICampus
{
    IEnumerator GetAllCampus(Action<CampusD> onCampusFound, Action<string> onSuccess, Action<string> onFaild);
    IEnumerator GetCampus(string campusName, Action<CampusD> onCampusFound, Action<string> onSuccess, Action<string> onFaild);
}

public interface IAddCampusHandler : ICampus
{
    IEnumerator AddNewCampus(CampusD campus, Action<string> onSuccess, Action<string> onFailed);
}