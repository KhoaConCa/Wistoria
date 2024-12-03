using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHistoryCardData
{
    public void Initialize(HistoryD history);

    public string Id { get; set; }
    public string Name { get; set; }
    public string StudentName { get; set; }
    public int TypeData { get; set; }
    public DateTime DateProcess { get; set; }
    public int Paper { get; set; }
}

public interface IHistoryCardDStudent
{
    public void Initialize(HistoryDStudent history);

    public string Id { get; set; }
    public PrinterDocDStudent PrinterDoc { get; set; }
    public PaymentDStudent Payment { get; set; }
    public int TypeData { get; set; }
    public DateTime? DateProcess { get; set; }
}

public interface IHistoryContainerData
{
    public void Initialize(string date, List<HistoryD> histories);

    public string Date { get; set; }
    public List<HistoryD> Histories { get; set; }
}

public interface IHistoryContainerDStudent
{
    public void Initialize(string date, List<HistoryDStudent> histories);

    public string Date { get; set; }
    public List<HistoryDStudent> Histories { get; set; }
}