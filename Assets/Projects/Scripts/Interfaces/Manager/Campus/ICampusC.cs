using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
