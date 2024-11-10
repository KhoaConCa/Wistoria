using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
