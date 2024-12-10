using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrinterDocCard : MonoBehaviour
{
    #region -- Methods --
    public void Initialize(PrinterDocDStudent data)
    {
        Id = data.Id;
        PaperSize = data.PaperSize;
        Side = data.Side;
        Orientation = data.Orientation;
        PageBegin = data.PageBegin;
        PageEnd = data.PageEnd;
        Copies = data.Copies;
        Color = data.Color;
        CompletionTime = data.CompletionTime;
        Process = data.Process;
        CreatedAt = data.CreatedAt;
        UpdatedAt = data.UpdatedAt;
    }
    #endregion

    #region -- Properties --

    public string Id { get; set; }
    public QueueD Queue { get; set; }
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
