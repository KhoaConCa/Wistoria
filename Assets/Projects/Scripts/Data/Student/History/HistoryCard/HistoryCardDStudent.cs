using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HistoryCardDStudent : MonoBehaviour, IHistoryCardDStudent
{
    public void Initialize(HistoryDStudent history)
    {
        Id = history.Id;
        Name = history.Name;
        PrinterDoc = history.PrinterDoc;
        PrinterDoc.Printer.ProcessLocateAt();
        Payment = history.Payment;
        TypeData = history.TypeData;
        DateProcess = history.DateProcess;
        Paper = history.Paper;
    }

    public string Id { get; set; }
    public string Name { get; set; }
    public PrinterDocDStudent PrinterDoc { get; set; }
    public PaymentDStudent Payment { get; set; }
    public int TypeData { get; set; }
    public DateTime DateProcess { get; set; }
    public int Paper { get; set; }
}
