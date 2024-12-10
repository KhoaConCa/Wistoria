using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPrinterDocView
{
    public QueueD GetQueue();
    public DocumentDStudent GetDocument();
    void SetPrinterDocCard(PrinterDocDStudent data);
    void TopUpPaper(int paperNeed);
    void SwitchHistory();
}
