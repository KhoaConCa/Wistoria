using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PrinterCardData : MonoBehaviour, IPrinterCardData
{
    #region -- Implements --

    public void Initialize(PrinterD printer)
    {
        PrinterID = printer._id;
        PrinterName = printer.PrinterName;
        PrinterType = printer.PrinterType;
        Description = printer.Description;
        LocateAt = printer.LocateAt;
        Paper = printer.Paper;
        Ink = printer.Ink;
        Status = printer.Status;
    }

    #region -- Properties --
    public string PrinterID { get; set; }
    public string PrinterName { get; set; }
    public string PrinterType { get; set; }
    public string Description { get; set; }
    public CampusD LocateAt { get; set; }
    public int Paper { get; set; }
    public int Ink { get; set; }
    public string Status { get; set; }
    #endregion

    #endregion
}
