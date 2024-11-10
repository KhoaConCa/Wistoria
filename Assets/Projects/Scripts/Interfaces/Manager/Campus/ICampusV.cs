using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICampusComponentAdder
{
    void AddComponentFromPrefab(Transform nameLocation, Transform roomLocation);
}

public interface ICampusViewSpawner
{
    void CreateCard(CampusD campus);
}

public interface ICampusDataSetter : ICampusComponentAdder
{
    void SetCampusName(string name);
    void SetCampusRoom(string room);
}

public interface ITransformUI
{
    void SetActiveCampusUI(GameObject targetCampus);
}
