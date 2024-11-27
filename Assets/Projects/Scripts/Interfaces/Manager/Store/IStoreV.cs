using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStoreComponentAdder
{
    void AddComponentFromPrefab(Transform paperLocation, Transform priceLocation);
}

public interface IStoreViewSpawner
{
    void CreateCard(StoreD store);
}

public interface IStoreDataSetter : IStoreComponentAdder
{
    void SetStoreData(IStoreCardData store);
}