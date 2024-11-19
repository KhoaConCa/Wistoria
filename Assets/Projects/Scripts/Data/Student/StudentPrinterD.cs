using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StudentPrinterD 
{
    public string PrinterName { get; set; }
    public string PrinterType { get; set; }
    public string Description { get; set; }
    public CampusD LocateAt { get; set; }
    public string Paper {  get; set; }
    public string Ink { get; set; }
    public string Status { get; set; }
}
