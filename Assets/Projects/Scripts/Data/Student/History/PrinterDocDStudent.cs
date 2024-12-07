using Newtonsoft.Json;
using System;

public class PrinterDocDStudent
{
    #region -- Methods --

    public void Initialize(PrinterDocCard cardData)
    {
        Id = cardData.Id;
        Printer = cardData.Printer;
        Document = cardData.Document;
        PaperSize = cardData.PaperSize;
        Orientation = cardData.Orientation;
        Side = cardData.Side;
        PageBegin = cardData.PageBegin;
        PageEnd = cardData.PageEnd;
        Copies = cardData.Copies;
        Color = cardData.Color;
        CompletionTime = cardData.CompletionTime;
        Process = cardData.Process;
    }

    #endregion

    #region -- Properties --

    [JsonProperty("_id")]
    public string Id { get; set; }

    [JsonProperty("PrintIn")]
    public object PrinterRaw { get; set; }

    [JsonIgnore]
    public string PrinterID { get; set; }

    [JsonIgnore]
    public StudentPrinterD Printer { get; set; }

    [JsonProperty("FileDocument")]
    public object DocumentRaw { get; set; }

    [JsonIgnore]
    public string DocumentID { get; set; }

    [JsonIgnore]
    public DocumentDStudent Document { get; set; }

    public string PaperSize { get; set; }
    public string Orientation { get; set; }
    public int Side { get; set; }
    public int PageBegin { get; set; }
    public int PageEnd { get; set; }
    public int Copies { get; set; }
    public string Color { get; set; }

    [JsonProperty("UpdateAt")]
    public DateTime? CompletionTime { get; set; }
    public string Process { get; set; }

    [JsonProperty("createdAt")]
    public DateTime CreatedAt { get; set; }

    [JsonProperty("updatedAt")]
    public DateTime UpdatedAt { get; set; }

    [JsonProperty("__v")]
    public string V { get; private set; } = "0";

    #endregion
}
