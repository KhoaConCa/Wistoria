using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HistoryCardDStudent : MonoBehaviour, IHistoryCardDStudent
{
    #region -- Implements

    public void Initialize(HistoryDStudent history)
    {
        Id = history.Id;
        if (history.PrinterDoc != null)
        {
            PrinterDoc = history.PrinterDoc;
        }
        
        if (history.Payment != null)
        {
            Payment = history.Payment;
            Payment.ProcessPaper();
        }

        TypeData = history.TypeData;
        DateProcess = history.DateProcess;
        Status = history.Status;
    }

    public string Id { get; set; }
    public PrinterDocDStudent PrinterDoc { get; set; }
    public PaymentDStudent Payment { get; set; }
    public int TypeData { get; set; }
    public DateTime? DateProcess { get; set; }
    public string Status { get; set; }

    #endregion
}
