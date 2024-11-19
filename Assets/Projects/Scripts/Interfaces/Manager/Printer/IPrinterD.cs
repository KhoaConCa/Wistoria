using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPrinterCardData
{
    string PrinterID { get; set; }
/*    string PrinterName { get; set; }
    string PrinterRoom { get; set; }*/
    void Initialize(string id/*, string name, string room*/);
}
