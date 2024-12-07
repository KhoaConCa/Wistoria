using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrinterDocCard : MonoBehaviour
{
    #region -- Properties --

    public string Id { get; set; }
    public StudentPrinterD Printer { get; set; }
    public DocumentDStudent Document { get; set; }
    public string PaperSize { get; set; }
    public string Orientation { get; set; }
    public int Side { get; set; }
    public int PageBegin { get; set; }
    public int PageEnd { get; set; }
    public int Copies { get; set; }
    public string Color { get; set; }
    public DateTime? CompletionTime { get; set; }
    public string Process { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string V { get; private set; } = "0";

    #endregion
}
