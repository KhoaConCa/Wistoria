using System;
using UnityEngine;

public class HistoryDStudent
{
    #region -- Methods --

    public void Intialize<T>(T data)
    {
        if (data is PaymentDStudent paymentData)
        {
            Id = paymentData.Id;
            PrinterDoc = null;
            Payment = paymentData;
            TypeData = 1;
            DateProcess = paymentData.CompletionTime;
        }
        else if (data is PrinterDocDStudent printerDocData)
        {
            Id = printerDocData.Id;
            PrinterDoc = printerDocData;
            Payment = null;
            TypeData = 0;
            DateProcess = printerDocData.CompletionTime;
        }
        else
        {
            Debug.LogError($"Error data type!");
        }
    }

    #endregion

    #region -- Properties --

    public string Id { get; set; }
    public PrinterDocDStudent PrinterDoc { get; set; }
    public PaymentDStudent Payment { get; set; }
    public int TypeData { get; private set; }
    public DateTime? DateProcess { get; set; }

    #endregion
}
