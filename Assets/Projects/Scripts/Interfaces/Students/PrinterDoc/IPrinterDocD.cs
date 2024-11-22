using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPrinterDocData
{
    string PrinterId { get; set; } // Printer ID
    PrinterDocD PrinterDocDetails { get; set; } // Holds the data in Property


    void Initialize(string printerId, PrinterDocD PrinterDocDetails);
}
