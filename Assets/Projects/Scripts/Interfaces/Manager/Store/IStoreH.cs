using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDetailStoreUpdateHandler
{
    IEnumerator UpdateStoreData(StoreD store, Action<string> onSuccess, Action<string> onFailed);
}

public interface IGetStoreHandler
{
    IEnumerator GetAllStore(Action<StoreD> onStoreFound, Action<string> onSuccess, Action<string> onFaild);
}

public interface IAddStoreHandler
{
    IEnumerator AddNewStore(StoreD store, Action<string> onSuccess, Action<string> onFailed);
}