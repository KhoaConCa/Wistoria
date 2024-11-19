using UnityEngine;

public class PrinterDocCardData : MonoBehaviour , IPrinterDocData
{
    #region -- Properties --

    public string PrinterId { get; set; } // Printer ID
    public string DocumentId { get; set; } // Document ID
    public string PaperSize { get; set; } // E.g., "A4", "A3"
    public int Side { get; set; } // Single-sided (1) or double-sided (2)
    public int PageBegin { get; set; } // Start page
    public int PageEnd { get; set; } // End page
    public int Copies { get; set; } // Number of copies
    public bool Color { get; set; } // True for color printing, false otherwise

    #endregion

    #region -- Methods --

    /// <summary>
    /// Initializes the PrinterDoc card data with the provided details.
    /// </summary>
    /// <param name="printerId">Printer ID</param>
    /// <param name="documentId">Document ID</param>
    /// <param name="paperSize">Paper size (e.g., A4, A3)</param>
    /// <param name="side">Single-sided or double-sided</param>
    /// <param name="pageBegin">Start page</param>
    /// <param name="pageEnd">End page</param>
    /// <param name="copies">Number of copies</param>
    /// <param name="color">True if color printing</param>
    public void Initialize(string printerId/*, string documentId, string paperSize, int side, int pageBegin, int pageEnd, int copies, bool color*/)
    {
        PrinterId = printerId;
/*        DocumentId = documentId;
        PaperSize = paperSize;
        Side = side;
        PageBegin = pageBegin;
        PageEnd = pageEnd;
        Copies = copies;
        Color = color;*/

    }

    #endregion
}
