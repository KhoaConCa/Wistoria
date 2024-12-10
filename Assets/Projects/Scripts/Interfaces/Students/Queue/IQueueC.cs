using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IQueueC
{
    void CallBackQueue(string id);
    void ProcessQueue(string id, int slotIndex);
    void AddQueue(QueueD queue, string id);

    void UpdatePrinterDoc(PrinterDocDStudent printerDoc, bool type = false);
}
