using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStudentPrinterView
{
    void SetStudentPrinterData(StudentPrinterD studentPrinter);
}
public interface ISpawnStudentPrinterView
{
    void CreateCard(StudentPrinterD studentPrinter, PrinterDocD printerDocDetails);
}

#region -- Interface for Setting Package Data in View --
/// <summary>
/// Interface for setting specific package data in a view component.
/// </summary>
#endregion
public interface ISetDataStudentPrinterView
{
    void SetStudentPrinterPrinterName(string printerName);

    void SetStudentPrinterCampusName(string campusName);
    void SetStudentPrinterStatus(string status);
    void AddComponentFromPrefab(Transform studentPrinterPrinterName, Transform studentPrinterCampusName, Transform studentPrinterStatus);
}