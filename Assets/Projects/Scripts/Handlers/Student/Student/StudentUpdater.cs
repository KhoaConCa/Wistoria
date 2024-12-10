using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using Utilities;
using System;

public class StudentUpdater : MonoBehaviour, IStudentPaper
{
    #region -- Implements --

    public IEnumerator GetStudentPaper(string studentId, Action<int> onSuccess, Action<string> onFailed)
    {
        string searchURL = $"{AllUrlStudent.findStudentById}/{studentId}";

        using (UnityWebRequest request = UnityWebRequest.Get(searchURL))
        {
            yield return request.SendWebRequest();

            MainData<StudentD> response = TransferObjectToData(request.downloadHandler.text);

            if (request.result == UnityWebRequest.Result.Success)
            {
                onSuccess?.Invoke(response.Data[0].Paper);
            }
            else
                onFailed?.Invoke(response.Message);
        }
    }

    public IEnumerator UpdateStudentPaper(string studentId, int newPaperCount, Action<string> onSuccess, Action<string> onError)
    {
        if (string.IsNullOrEmpty(studentId))
        {
            onError?.Invoke("Student ID is null or empty. Cannot update paper count.");
            yield break;
        }

        string url = $"{AllUrlStudent.updateStudentById}/{studentId}";

        string json = $"{{ \"Paper\": {TransferDataToJson(newPaperCount)} }}";

        Debug.Log(json);

        using (UnityWebRequest request = new UnityWebRequest(url, "PATCH"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);

            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            MainData<StudentD> response = TransferObjectToData(request.downloadHandler.text);

            if (request.result == UnityWebRequest.Result.Success)
                onSuccess?.Invoke(response.Message);
            else
                onError?.Invoke(response.Message);
        }

        yield return new WaitForSeconds(2f);
    }

    #endregion

    #region -- Methods --

    public string TransferDataToJson(int paper)
    {
        return MainHandler.ToJson<int>(paper);
    }

    public MainData<StudentD> TransferObjectToData(string response)
    {
        MainData<StudentD> mainData = JsonConvert.DeserializeObject<MainData<StudentD>>(response);
        mainData.Initialize();
        return mainData;
    }

    #endregion
}
