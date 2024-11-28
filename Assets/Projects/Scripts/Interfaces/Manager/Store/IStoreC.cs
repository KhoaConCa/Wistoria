using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGetStoreCommand
{
    void OnStoreFound(StoreD store);
}

public interface IModifyStoreCommand
{
    void ClickCardToModify();
}

public interface IStoreDetailCommand
{
    void DisplayStoreDetails(IStoreCardData cardData);
}

public interface IAddStoreCommand
{
    void ClickAddButton();
}
