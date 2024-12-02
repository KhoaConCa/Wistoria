using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public interface IHistoryComponentAdder
{
    void AddComponentFromPrefab(Transform valueLocation);
}

public interface IHistoryViewSpawner
{
    void CreateContainer(string dateTime, List<HistoryD> histories);
}

public interface ICardHistoryViewSpawner
{
    void GetData(List<HistoryD> histories);
    void CreateCard(HistoryD history);
}

public interface IHistoryDataSetter : IHistoryComponentAdder
{
    void SetHistoryData(string value);
}