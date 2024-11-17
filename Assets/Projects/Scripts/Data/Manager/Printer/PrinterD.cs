using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PrinterD
{
    #region -- Overrides --

    public override string ToString()
    {
        return $"Printer ID: {_id}, Printer Name: {PrinterName}, Room: {LocateAt}";
    }

    #endregion

    #region -- Properties --

    public string _id { get; set; }
    public string PrinterName { get; set; }
    public string PrinterType { get; set; }
    public string Description { get; set; }
    public CampusD LocateAt { get; set; }
    public int Paper { get; set; }
    public int Ink { get; set; }
    public string Status { get; set; }
    public DateTime createdAt { get; set; }
    public DateTime updatedAt { get; set; }
    public string __v { get; set; }

    #endregion
}


