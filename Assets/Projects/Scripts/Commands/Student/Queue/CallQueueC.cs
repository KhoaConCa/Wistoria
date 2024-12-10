using Newtonsoft.Json.Linq;
using System;
using UnityEngine;
using Utilities;

public class CallQueueC : MonoBehaviour, IQueueC
{
    #region -- Implements --

    public void CallBackQueue(string id)
    {
        StartCoroutine(_queueH.SearchQueueById(id, onSuccess =>
        {
            _queueV.SetNewQueue(onSuccess);
            _queueV.IsChecking = true;
        }, onFailed =>
        {
            MainView.OnFailed(onFailed);
        }));
    }

    public void ProcessQueue(string id, int slotIndex)
    {
        StartCoroutine(_queueH.UpdateNullSlotQueue(id, slotIndex, onSuccess =>
        {
            MainView.OnDebugged("Đã hoàn thành in tài liệu!");
        }, onFailed =>
        {
            MainView.OnDebugged("In tài liệu thất bại!");
        }));
    }

    public void AddQueue(QueueD queue, string id)
    {
        int slotIndex = 0;
        string json = "";

        switch (queue.SlotRemaining)
        {
            case 3:
                json = CreateJson(QueueSlot.FirstSlot.ToString(), id);
                slotIndex = 3;
                break;

            case 2:
                json = CreateJson(QueueSlot.SecondSlot.ToString(), id);
                slotIndex = 2;
                break;

            case 1:
                json = CreateJson(QueueSlot.ThirdSlot.ToString(), id);
                slotIndex = 1;
                break;
        }

        if (string.IsNullOrEmpty(json)) return;

        StartCoroutine(_queueH.UpdateSlotQueue(json, queue.Id, slotIndex, onSuccess =>
        {
            _queueV.SetNewQueue(onSuccess);
            _queueV.IsChecking = true;
        }, onFailed =>
        {
            MainView.OnFailed(onFailed);
        }));
    }

    public void UpdatePrinterDoc(PrinterDocDStudent printerDoc, bool type = false)
    {
        _printerDoc = printerDoc;

        if (!type)
        {
            JObject printerDocFailed = new JObject()
            {
                ["UpdateAt"] = DateTime.Now.ToString("MM/dd/yyyy"),
                ["Process"] = PrinterDocStatus.Failed.ToString(),
            };

            _json = printerDocFailed.ToString();

            UpdatePaper();
            MainView.OnFailed("In tài liệu thất bại!");
            return;
        }

        JObject printerDocSuccess = new JObject()
        {
            ["UpdateAt"] = DateTime.Now.ToString("HH:mm:ss MM/dd/yyyy"),
            ["Process"] = PrinterDocStatus.Done.ToString(),
        };

        _json = printerDocSuccess.ToString();

        UpdatePrinterDoc();
        MainView.OnSuccess("Đã hoàn thành in tài liệu!");
    }

    #endregion

    #region -- Methods --

    private void Awake()
    {
        AddComponent();
        GetComponent();
    }

    #region -- Add Component --
    private void AddComponent()
    {
        if (_queueH == null)
            _queueH = this.gameObject.AddComponent<UpdateSlotQueueH>();
    }
    #endregion

    #region -- Get Component --
    private void GetComponent()
    {
        if (_queueV == null)
            _queueV = this.gameObject.GetComponent<CallQueueV>();
    }
    #endregion

    #region -- Create Json --
    private string CreateJson(string name, string value)
    {
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(value))
        {
            Debug.LogError("Name or Value is none!");
            return null;
        }

        JObject jsonObject = new JObject()
        {
            [$"{name}"] = value
        };

        return jsonObject.ToString();
    }
    #endregion

    private void UpdatePaper()
    {
        StartCoroutine(_queueH.UpdatePaper(_printerDoc.Document.Student, onSuccess =>
        {
            MainView.OnDebugged(onSuccess);
            UpdatePrinterDoc();

        }, onFailed =>
        {
            MainView.OnDebugged(onFailed);
            MainView.OnReset(UpdatePaper, onFailed);
        }));
    }

    private void UpdatePrinterDoc()
    {
        StartCoroutine(_queueH.UpdatePrinterDoc(_json, _printerDoc.Id, onSuccess =>
        {
            MainView.OnDebugged(onSuccess);
            Destroy(this.gameObject);
        }, onFailed =>
        {
            MainView.OnDebugged(onFailed);
            MainView.OnReset(UpdatePrinterDoc, onFailed);
        }));
    }

    #endregion

    #region -- Fields --

    private IQueueH _queueH;
    private IQueueV _queueV;

    private string _json;
    private PrinterDocDStudent _printerDoc;

    #endregion
}
