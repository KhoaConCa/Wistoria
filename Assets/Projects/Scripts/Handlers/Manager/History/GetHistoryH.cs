using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using Utilities;

public class GetHistoryH : MonoBehaviour, IHistoryHandler
{
    #region -- Implements --

    public IEnumerator GetAllHistory(Action<string, List<HistoryD>> onHistoryFound, Action<string> onSuccess, Action<string> onFaild)
    {
        _onHistoryFound = onHistoryFound;

        if(_historyDs.Count > 0)
            _historyDs.Clear();

        using (UnityWebRequest request = UnityWebRequest.Get(AllUrlManager.getAllHistory))
        {

            yield return request.SendWebRequest();

            MainData<PrinterDocDManager> response = TransferHPrinterToData(request.downloadHandler.text);

            if (request.result == UnityWebRequest.Result.Success)
            {
                onSuccess?.Invoke(response.Message);

                MergeData(response.Data);
            }
            else
                onFaild?.Invoke(response.Message);
        }

        using (UnityWebRequest request = UnityWebRequest.Get(AllUrlManager.getAllPayment))
        {

            yield return request.SendWebRequest();

            MainData<PaymentDManager> response = TransferHPaymentToData(request.downloadHandler.text);

            if (request.result == UnityWebRequest.Result.Success)
            {
                onSuccess?.Invoke(response.Message);

                GetPackage(response);
            }
            else
                onFaild?.Invoke(response.Message);
        }

        SortDateTime();
        SendData();
    }
    public IEnumerator GetHistoryByPrinter(Action<string, List<HistoryD>> onHistoryFound, Action<string> onSuccess, Action<string> onFaild)
    {
        _onHistoryFound = onHistoryFound;

        if (_historyDs.Count > 0)
            _historyDs.Clear();

        using (UnityWebRequest request = UnityWebRequest.Get(AllUrlManager.getAllHistory))
        {

            yield return request.SendWebRequest();

            MainData<PrinterDocDManager> response = TransferHPrinterToData(request.downloadHandler.text);

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
    public IEnumerator GetHistoryByPayment(Action<string, List<HistoryD>> onHistoryFound, Action<string> onSuccess, Action<string> onFaild)
    {
        _onHistoryFound = onHistoryFound;

        if (_historyDs.Count > 0)
            _historyDs.Clear();

        using (UnityWebRequest request = UnityWebRequest.Get(AllUrlManager.getAllPayment))
        {

            yield return request.SendWebRequest();

            MainData<PaymentDManager> response = TransferHPaymentToData(request.downloadHandler.text);

            if (request.result == UnityWebRequest.Result.Success)
            {
                onSuccess?.Invoke(response.Message);

                GetPackage(response);
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

    private MainData<PaymentDManager> TransferHPaymentToData(string response)
    {
        MainData<PaymentDManager> mainData = JsonConvert.DeserializeObject<MainData<PaymentDManager>>(response);
        mainData.Initialize();
        return mainData;
    }

    private MainData<PrinterDocDManager> TransferHPrinterToData(string response)
    {
        MainData<PrinterDocDManager> mainData = JsonConvert.DeserializeObject<MainData<PrinterDocDManager>>(response);
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
        foreach (T data in datas)
        {
            HistoryD historyD = new HistoryD();
            historyD.Intialize(data);
            _historyDs.Add(historyD);
        }
    }

    private void SortDateTime()
    {
        _historyDs = _historyDs
        .OrderBy(d => d == null)
        .ThenByDescending(d => d?.DateProcess)
        .ToList();
    }

    private void GetPackage(MainData<PaymentDManager> datas)
    {
        foreach (var data in datas.Data) 
        {
            data.ProcessPaper();

            if (data.PaperID != null)
                StartCoroutine(GetPackageById(data.PaperID, onSuccess =>
                {
                    data.UpdatePaper(onSuccess);

                    MergeData(datas.Data);

                    SortDateTime();

                    SendData();
                }));
        }
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

        foreach (HistoryD historyD in _historyDs)
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
        List<HistoryD> historyDs = new List<HistoryD>();

        foreach (HistoryD history in _historyDs)
        {
            string date = history.DateProcess?.ToString("MM/yyyy");

            if ( date != _containers[count])
            {
                _onHistoryFound?.Invoke(_containers[count], new List<HistoryD>(historyDs));
                historyDs.Clear();
                count++;
            }
            historyDs.Add(history);
        }
        _onHistoryFound?.Invoke(_containers[count], new List<HistoryD>(historyDs));
    }


    #endregion

    #region -- Fields --

    private Action<string, List<HistoryD>> _onHistoryFound;

    private List<HistoryD> _historyDs = new List<HistoryD>();
    private List<string> _containers = new List<string>();

    #endregion
}
