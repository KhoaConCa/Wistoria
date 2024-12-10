using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IQueueH
{
    IEnumerator SearchQueueById(string idQueue, Action<QueueD> onSuccess, Action<string> onFailed);
    IEnumerator UpdateNullSlotQueue(string idQueue, int slot, Action<QueueD> onSuccess, Action<string> onFailed);
    IEnumerator UpdateSlotQueue(string json, string idQueue, int slot, Action<QueueD> onSuccess, Action<string> onFailed);
    IEnumerator UpdatePaper(StudentD studentD, Action<string> onSuccess, Action<string> onFailed);
    IEnumerator UpdatePrinterDoc(string json, string id, Action<string> onSuccess, Action<string> onFailed);
}
