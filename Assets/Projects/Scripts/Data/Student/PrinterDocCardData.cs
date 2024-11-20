using UnityEngine;

public class PrinterDocCardData : MonoBehaviour , IPrinterDocData
{
    #region -- Properties --

    public string PrinterId { get; set; } // Printer ID
    public PrinterDocD PrinterDocDetails { get; set; } // Holds the data in Property


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
    public void Initialize(string printerId, PrinterDocD printerDocDetails)
    {
        PrinterId = printerId;
        PrinterDocDetails = printerDocDetails;
    }

    #endregion
}
