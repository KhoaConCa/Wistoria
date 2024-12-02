using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HistoryD
{
    public void Intialize<T>(T data)
    {
        if (data is PaymentDManager paymentData)
        {
            Id = paymentData.Id;
            Name = $"Thanh toán gói giấy qua MoMo";
            StudentName = paymentData.Student.Name;
            TypeData = 0;
            DateProcess = paymentData.CompletionTime;
            Paper = paymentData.Paper;
        }
        else if (data is HistoryDManager historyData)
        {
            Id = historyData.Id;
            historyData.Printer.ProcessLocateAt();
            Name = $"Thực hiện in ấn {historyData.Printer.PrinterName.Truncate(10)} / {historyData.Printer.LocateAt.Name} - {historyData.Printer.LocateAt.Room}";
            StudentName = historyData.Document.Student.Name;
            TypeData = 1;
            DateProcess = historyData.CompletionTime;
            Paper = (historyData.PageEnd - historyData.PageBegin + 1) / historyData.Side * historyData.Copies;
        }
        else
        {
            Debug.LogError($"Error data type!");
        }
    }

    public string Id {  get; set; }
    public string Name { get; set; }
    public string StudentName { get; set; }
    public int TypeData { get; private set; }
    public DateTime DateProcess { get; set; }
    public int Paper { get; set; }
}
