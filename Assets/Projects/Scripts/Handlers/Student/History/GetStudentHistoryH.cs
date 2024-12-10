using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class GetStudentHistoryH : MonoBehaviour, IHistoryStudentHandler
{
    #region -- Implements --

    public IEnumerator GetAllHistoryByPrinter(string id, Action<string, List<HistoryDStudent>> onHistoryFound, 
        Action<string> onSuccess, Action<string> onFailed)
    {
        _onHistoryFound = onHistoryFound;

        if (_historyDs.Count > 0)
            _historyDs.Clear();

        string urlInProgress = AllUrlStudent.searchHistoryByID + $"?id={id}&status=In+Progress";
        string urlDone = AllUrlStudent.searchHistoryByID + $"?id={id}&status=Done";

        using (UnityWebRequest request = UnityWebRequest.Get(urlInProgress))
        {
            yield return request.SendWebRequest();

            MainData<PrinterDocDStudent> response = TransferHPrinterToData(request.downloadHandler.text);

            if (request.result == UnityWebRequest.Result.Success)
            {
                onSuccess?.Invoke(response.Message);

                MergeData(response.Data);
            }
            else
                onFailed?.Invoke(response.Message);
        }

        using (UnityWebRequest request = UnityWebRequest.Get(urlDone))
        {
            yield return request.SendWebRequest();

            MainData<PrinterDocDStudent> response = TransferHPrinterToData(request.downloadHandler.text);

            if (request.result == UnityWebRequest.Result.Success)
            {
                onSuccess?.Invoke(response.Message);

                MergeData(response.Data);
            }
            else
                onFailed?.Invoke(response.Message);
        }

        SortDateTime();
        SendData();
    }

    public IEnumerator GetHistoryByPrinter(string id, string status, 
        Action<string, List<HistoryDStudent>> onHistoryFound, Action<string> onSuccess, Action<string> onFailed)
    {
        _onHistoryFound = onHistoryFound;

        if (_historyDs.Count > 0)
            _historyDs.Clear();

        string url = AllUrlStudent.searchHistoryByID + $"?id={id}&status={status}";
        Debug.Log(url);

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            MainData<PrinterDocDStudent> response = TransferHPrinterToData(request.downloadHandler.text);

            if (request.result == UnityWebRequest.Result.Success)
            {
                onSuccess?.Invoke(response.Message);

                MergeData(response.Data);
                SortDateTime();

                SendData();
            }
            else
                onFailed?. Invoke(response.Message);
        }
        
    }

    public IEnumerator GetHistoryByPayment(string id, Action<string, List<HistoryDStudent>> onHistoryFound, 
        Action<string> onSuccess, Action<string> onFaild)
    {
        _onHistoryFound = onHistoryFound;

        if (_historyDs.Count > 0)
            _historyDs.Clear();

        using (UnityWebRequest request = UnityWebRequest.Get(AllUrlStudent.searchPaymentByID + id))
        {

            yield return request.SendWebRequest();

            MainData<PaymentDStudent> response = TransferHPaymentToData(request.downloadHandler.text);

            if (request.result == UnityWebRequest.Result.Success)
            {
                onSuccess?.Invoke(response.Message);

                MergeData(response.Data);
                SortDateTime();

                SendData();
            }
            else
                onFaild?.Invoke(response.Message);
        }
    }

    #endregion

    #region -- Methods --

    public IEnumerator GetPackageById(string id, Action<PackageJsonD> onPackageFound)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(AllUrlStudent.searchPackageById + id))
        {

            yield return request.SendWebRequest();

            MainData<PackageJsonD> response = TransferPackageToData(request.downloadHandler.text);

            if (request.result == UnityWebRequest.Result.Success)
            {
                onPackageFound?.Invoke(response.Data[0]);
            }
        }
    }

    private MainData<PaymentDStudent> TransferHPaymentToData(string response)
    {
        MainData<PaymentDStudent> mainData = JsonConvert.DeserializeObject<MainData<PaymentDStudent>>(response);
        mainData.Initialize();
        return mainData;
    }
    private MainData<PrinterDocDStudent> TransferHPrinterToData(string response)
    {
        MainData<PrinterDocDStudent> mainData = JsonConvert.DeserializeObject<MainData<PrinterDocDStudent>>(response);
        mainData.Initialize();
        return mainData;
    }

    private MainData<PackageJsonD> TransferPackageToData(string response)
    {
        MainData<PackageJsonD> mainData = JsonConvert.DeserializeObject<MainData<PackageJsonD>>(response);
        mainData.Initialize();
        return mainData;
    }

    private void MergeData<T>(List<T> datas)
    {
        if (datas == null) return;
        if (datas.Count <= 0) return;

        foreach (T data in datas)
        {
            HistoryDStudent historyD = new HistoryDStudent();
            historyD.Intialize(data);

            _historyDs.Add(historyD);
        }
    }

    private void SortDateTime()
    {
        _historyDs = _historyDs.OrderByDescending(date => date.DateProcess).ToList();
    }

    private void SendData()
    {
        if (_historyDs.Count <= 0) return;

        GetListContainer();
        InvokeListData();
    }

    private void GetListContainer()
    {
        if (_containers.Count > 0) _containers.Clear();

        foreach (HistoryDStudent historyD in _historyDs)
        {
            string date = historyD.DateProcess?.ToString("MM/yyyy");
            if (!_containers.Contains(date))
            {
                _containers.Add(date);
            }
        }
    }

    private void InvokeListData()
    {
        int count = 0;
        List<HistoryDStudent> historyDs = new List<HistoryDStudent>();

        foreach (HistoryDStudent history in _historyDs)
        {
            string date = history.DateProcess?.ToString("MM/yyyy");

            if (date != _containers[count])
            {
                _onHistoryFound?.Invoke(_containers[count], new List<HistoryDStudent>(historyDs));
                historyDs.Clear();
                count++;
            }
            historyDs.Add(history);
        }
        _onHistoryFound?.Invoke(_containers[count], new List<HistoryDStudent>(historyDs));
    }

    #endregion

    #region -- Fields --

    private Action<string, List<HistoryDStudent>> _onHistoryFound;

    private List<HistoryDStudent> _historyDs = new List<HistoryDStudent>();
    private List<string> _containers = new List<string>();

    #endregion
}
