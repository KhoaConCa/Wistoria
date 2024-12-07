using UnityEngine;

public class PrinterDocCardData : MonoBehaviour, IPrinterDocData
{
    #region -- Implements --

    public void Initialize(StudentPrinterD printerD)
    {
        PrinterD = printerD;
    }

    #region -- Properties --
    public StudentPrinterD PrinterD { get; set; }
    #endregion

    #endregion


}