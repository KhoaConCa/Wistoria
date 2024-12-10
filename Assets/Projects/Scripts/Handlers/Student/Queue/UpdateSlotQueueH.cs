using Newtonsoft.Json;
using Spine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Utilities;

public class UpdateSlotQueueH : MonoBehaviour, IQueueH
{
    #region -- Implements --

    public IEnumerator SearchQueueById(string idQueue, Action<QueueD> onSuccess, Action<string> onFailed)
    {
        string url = AllUrlStudent.searchQueueById + idQueue;

        using (UnityWebRequest request = new UnityWebRequest(url, "GET"))
        {
            request.downloadHandler = new DownloadHandlerBuffer();

            yield return request.SendWebRequest();

            MainData<QueueD> response = TransferObjectToData<QueueD>(request.downloadHandler.text);

            if (request.result == UnityWebRequest.Result.Success)
            {
                response.Data[0] = InitializeString(response.Data[0]);
                onSuccess?.Invoke(response.Data[0]);
            }
            else
                onFailed?.Invoke(response.Message);
        }
    }


    public IEnumerator UpdateNullSlotQueue(string idQueue, int slot, Action<QueueD> onSuccess, Action<string> onFailed)
    {
        string url = $"{AllUrlStudent.updateNullSlot}id={idQueue}&slot={slot}";

        using (UnityWebRequest request = new UnityWebRequest(url, "PATCH"))
        {
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            MainData<QueueD> response = TransferObjectToData<QueueD>(request.downloadHandler.text);

            if (request.result == UnityWebRequest.Result.Success)
            {
                response.Data[0] = InitializeString(response.Data[0]);
                onSuccess?.Invoke(response.Data[0]);
            }
            else
                onFailed?.Invoke(response.Message);
        }
    }

    public IEnumerator UpdateSlotQueue(string json, string idQueue, int slot, Action<QueueD> onSuccess, Action<string> onFailed)
    {
        string url = "";

        switch (slot)
        {
            case 3:
                url += AllUrlStudent.updateFirstSlot;
                break;

            case 2:
                url += AllUrlStudent.updateSecondSlot;
                break;

            case 1:
                url += AllUrlStudent.updateThirdSlot;
                break;
        }

        url += idQueue;

        using (UnityWebRequest request = new UnityWebRequest(url, "PATCH"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);

            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            MainData<QueueD> response = TransferObjectToData<QueueD>(request.downloadHandler.text);

            if (request.result == UnityWebRequest.Result.Success)
            {
                response.Data[0] = InitializeString(response.Data[0]);
                onSuccess?.Invoke(response.Data[0]);
            }
            else
                onFailed?.Invoke(response.Message);
        }
    }

    public IEnumerator UpdatePaper(StudentD studentD, Action<string> onSuccess, Action<string> onFailed)
    {
        string url = $"{AllUrlStudent.updateStudentById}/{studentD.Id}";
        string json = TransferDataToJson(studentD);

        using (UnityWebRequest request = new UnityWebRequest(url, "PATCH"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);

            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            MainData<StudentD> response = TransferObjectToData<StudentD>(request.downloadHandler.text);

            if (request.result == UnityWebRequest.Result.Success)
                onSuccess?.Invoke(response.Message);
            else
                onFailed?.Invoke(response.Message);
        }
    }

    public IEnumerator UpdatePrinterDoc(string json, string id, Action<string> onSuccess, Action<string> onFailed)
    {
        string url = AllUrlStudent.updatePrinterDoc + id;

        using (UnityWebRequest request = new UnityWebRequest(url, "PATCH"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);

            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            MainData<QueueD> response = TransferObjectToData<QueueD>(request.downloadHandler.text);

            if (request.result == UnityWebRequest.Result.Success)
            {
                response.Data[0] = InitializeString(response.Data[0]);
                onSuccess?.Invoke(response.Message);
            }
            else
                onFailed?.Invoke(response.Message);
        }
    }

    #endregion

    #region -- Methods --

    private MainData<T> TransferObjectToData<T>(string response)
    {
        MainData<T> mainData = JsonConvert.DeserializeObject<MainData<T>>(response);
        mainData.Initialize();
        return mainData;
    }

    private QueueD InitializeString(QueueD queue)
    {
        if (queue.FirstSlotRaw != null)
            queue.FirstSlotId = queue.FirstSlotRaw.ToString();

        if (queue.SecondSlotRaw != null)
            queue.SecondSlotId = queue.SecondSlotRaw.ToString();

        if (queue.ThirdSlotRaw != null)
            queue.ThirdSlotId = queue.ThirdSlotRaw.ToString();

        return queue;
    }

    public string TransferDataToJson<T>(T data)
    {
        return MainHandler.ToJson<T>(data);
    }

    #endregion
}
