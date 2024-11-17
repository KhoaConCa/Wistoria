using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPrinterCardData
{
    string PrinterID { get; set; }
    string PrinterName { get; set; }
    string PrinterType { get; set; }
    string Description { get; set; }
    CampusD LocateAt { get; set; }
    int Paper { get; set; }
    int Ink { get; set; }
    string Status { get; set; }
    void Initialize(PrinterD printer);
}
