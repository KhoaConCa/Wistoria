using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataCampusTransferHandler
{
    void TransferData(string response);
}

public interface IDetailCampusUpdateHandler
{
    IEnumerator UpdateCampusData(CampusD campus, Action<CampusD> onSuccess, Action<CampusD> onFailed);

    IEnumerator GetUniqueName(Action<List<string>> onNameCampus);
}

public interface IGetCampusHandler : IDataCampusTransferHandler
{
    IEnumerator GetAllCampus(Action<CampusD> onCampusFound);
    IEnumerator GetCampus(string campusName, Action<CampusD> onCampusFound);
    IEnumerator GetUniqueName(Action<List<string>> onNameCampus);
}

public interface IAddCampusHandler
{
    IEnumerator AddNewCampus(CampusD campus, Action<CampusD> onSuccess);
}