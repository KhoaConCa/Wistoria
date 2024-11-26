using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPrinterComponentAdder
{
    void AddComponentFromPrefab(Transform nameLocation, Transform printerLocation, Transform locationAt);
}

public interface IPrinterViewSpawner
{
    void CreateCard(PrinterD campus);
}

public interface IPrinterDataSetter : IPrinterComponentAdder
{
    void SetDataPrinterCard(PrinterD printer);
}