using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPrinterComponentAdder
{
    void AddComponentFromPrefab(Transform nameLocation, Transform printerLocation, Transform roomLocation);
}

public interface IPrinterViewSpawner
{
    void CreateCard(PrinterD Printer);
}

public interface IPrinterDataSetter : IPrinterComponentAdder
{
    void SetDataPrinterCard(PrinterD Printer);
}

public interface ITransformUIPrinter
{
    void SetActivePrinterUI(GameObject targetPrinter);
}