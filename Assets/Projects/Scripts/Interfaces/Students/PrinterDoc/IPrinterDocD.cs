using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPrinterDocData
{
    string PrinterId { get; set; } // Printer ID
    string DocumentId { get; set; } // Document ID
    string PaperSize { get; set; } // E.g., "A4", "A3"
    int Side { get; set; } // Single-sided (1) or double-sided (2)
    int PageBegin { get; set; } // Start page
    int PageEnd { get; set; } // End page
    int Copies { get; set; } // Number of copies
    bool Color { get; set; } // True for color printing, false otherwise

    void Initialize(string printerId/*, string documentId, string paperSize, int side, int pageBegin, int pageEnd, int copies, bool color*/);
}
