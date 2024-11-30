using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public interface IHistoryHandler
{
    public IEnumerator GetAllHistory(Action<string, List<HistoryD>> onHistoryFound, Action<string> onSuccess, Action<string> onFaild);
    public IEnumerator GetHistoryByPrinter(Action<string, List<HistoryD>> onHistoryFound, Action<string> onSuccess, Action<string> onFaild);
    public IEnumerator GetHistoryByPayment(Action<string, List<HistoryD>> onContainerFound, Action<string> onSuccess, Action<string> onFaild);
}
