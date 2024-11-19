using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGetCampusCommand
{
    void OnCampusFound(CampusD campus);
}

public interface IModifyCampusCommand
{
    void ClickCard();
    void SetupButton();
}

public interface ICampusDetailCommand
{
    void DisplayCampusDetails(ICampusCardData cardData);
}

public interface IAddCampusCommand
{
    void ClickAddButton();
}
