using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HistoryDManager
{
    #region -- Methods --

    public void Initialize()
    {

    }

    #endregion

    #region -- Properties --

    [JsonProperty("_id")]
    public string Id { get; set; }
    [JsonProperty("PrintIn")]
    public PrinterD Printer { get; set; }
    [JsonProperty("FileDocument")]
    public DocumentDManager Document { get; set; }

    public string PaperSize { get; set; }
    public string Orientation { get; set; }
    public int Side { get; set; }
    public int PageBegin { get; set; }
    public int PageEnd { get; set; }
    public int Copies { get; set; }
    public string Color { get; set; }

    [JsonProperty("UpdateAt")]
    public DateTime CompletionTime { get; set; }

    [JsonProperty("createdAt")]
    public DateTime CreatedAt { get; set; }

    [JsonProperty("updatedAt")]
    public DateTime UpdatedAt { get; set; }

    [JsonProperty("__v")]
    public string V { get; private set; } = "0";

    #endregion
}
