using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPrinterComponentAdder
{
    void AddComponentFromPrefab(Transform nameLocation, Transform roomLocation);
}

public interface IPrinterViewSpawner
{
    void CreateCard(PrinterD Printer);
}

public interface IPrinterDataSetter : IPrinterComponentAdder
{
    void SetPrinterName(string name);
    void SetPrinterRoom(string room);
}

public interface ITransformUIPrinter
{
    void SetActivePrinterUI(GameObject targetPrinter);
}