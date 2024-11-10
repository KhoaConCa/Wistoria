using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataTransferHandler
{
    void TransferData(string response);
}

public interface IDetailUpdateHandler
{
    IEnumerator UpdateCampusData(CampusD campus, Action<CampusD> onSuccess, Action onFailed);
}

public interface IGetCampusHandler : IDataTransferHandler
{
    IEnumerator GetAllCampus(Action<CampusD> onCampusFound);
    IEnumerator GetCampus(string campusName, Action<CampusD> onCampusFound);
}
