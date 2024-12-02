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

public interface IHistoryStudentHandler
{
    public IEnumerator GetHistoryByPrinter(string id, string status, Action<string, List<HistoryDStudent>> onHistoryFound, Action<string> onSuccess, Action<string> onFaild);
    public IEnumerator GetHistoryByPayment(string id, Action<string, List<HistoryDStudent>> onContainerFound, Action<string> onSuccess, Action<string> onFaild);
}
