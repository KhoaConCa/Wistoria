using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPrinterDocData
{
    string PrinterId { get; set; }
    PrinterDocD PrinterDocDetails { get; set; }
    void Initialize(string printerId, PrinterDocD PrinterDocDetails);
}
