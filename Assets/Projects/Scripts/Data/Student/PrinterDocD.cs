using System;

[System.Serializable]
public class PrinterDocD
{
    public string Id { get; set; } // Maps to PK_ID
    public string PrinterId { get; set; } // Maps to FK_PrinterID
    public string DocumentId { get; set; } // Maps to FK_DocID
    public string PaperSize { get; set; } // E.g., "A4", "A3"
    public int Side { get; set; } // Single-sided or double-sided (1 or 2)
    public int PageBegin { get; set; } // Start page for printing
    public int PageEnd { get; set; } // End page for printing
    public int Copies { get; set; } // Number of copies
    public bool Color { get; set; } // True if color printing, false otherwise

}
