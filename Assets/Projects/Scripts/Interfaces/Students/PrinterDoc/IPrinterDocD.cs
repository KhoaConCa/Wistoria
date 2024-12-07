using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPrinterDocData
{
    StudentPrinterD PrinterD { get; set; }
    void Initialize(StudentPrinterD printer);
}
