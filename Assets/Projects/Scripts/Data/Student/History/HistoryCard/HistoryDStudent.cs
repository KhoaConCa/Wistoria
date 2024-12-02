using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HistoryDStudent
{
    public void Intialize<T>(T data)
    {
        if (data is PaymentDStudent paymentData)
        {
            Id = paymentData.Id;
            Name = $"Thanh toán gói giấy qua MoMo";
            PrinterDoc = null;
            Payment = paymentData;
            TypeData = 0;
            DateProcess = paymentData.CompletionTime;
            Paper = paymentData.Paper;
        }
        else if (data is PrinterDocDStudent printerDocData)
        {
            Id = printerDocData.Id;
            Name = printerDocData.Document.Name.Truncate(15);
            PrinterDoc = printerDocData;
            Payment = null;
            TypeData = 1;
            DateProcess = printerDocData.CompletionTime;
            Paper = ((printerDocData.PageEnd - printerDocData.PageBegin + 1) / printerDocData.Side) * printerDocData.Copies;
        }
        else
        {
            Debug.LogError($"Error data type!");
        }
    }

    public string Id { get; set; }
    public string Name { get; set; }
    public PrinterDocDStudent PrinterDoc { get; set; }
    public PaymentDStudent Payment { get; set; }
    public int TypeData { get; private set; }
    public DateTime DateProcess { get; set; }
    public int Paper { get; set; }
}
