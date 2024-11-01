using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public interface IPackageView
{
    void SetPackageData(PackageD package);
}

public interface ISetDataPackageView
{
    void SetPackagePrice(string price);
    void SetPackagePaper(string paper);
    void AddComponentFromPrefab(Transform priceLocation, Transform paperLocation);
}