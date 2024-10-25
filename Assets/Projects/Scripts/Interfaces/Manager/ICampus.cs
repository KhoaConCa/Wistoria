using System;
using System.Collections;
using System.Collections.Generic;

public interface ICampusCommand
{
    void Initialization();
}

public interface IGetCampusCommand : ICampusCommand
{
    void ClickFindButton();
    string GetSelectedCampusName();
    void OnCampusFound(CampusD campus);
}

public interface ICampusHandler
{

}

public interface IGetCampusHandler : ICampusHandler
{
    IEnumerator GetCampus(string campusName, Action<CampusD> onCampusFound);
}


public interface ICampusView
{
    void SetCampusData(CampusD campus);
}