using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Text;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using Utilities;

public class UploadDocumentH : MonoBehaviour, IUploadDocumentHandler
{
    #region -- Implements --

    /// <summary>
    /// Initiates the coroutine to upload document properties.
    /// </summary>
    /// <param name="filePath">The file path of the document to be uploaded.</param>
    /// <param name="onDocumentIdReceived">Callback to handle the newly created document ID.</param>
    public void UploadDocumentProperties(string filePath, Action<string> onSuccess, Action<string> onFaild)
    {
        if (!gameObject.activeSelf)
        {
            Debug.LogWarning("GameObject is inactive. Activating temporarily to start coroutine.");
            gameObject.SetActive(true);
        }

        StartCoroutine(GetStudentByID(MainUser.STUDENT_ID, onSucces =>
        {
            // Gather file attributes
            FileInfo fileInfo = new FileInfo(filePath);
            string fileName = fileInfo.Name;
            string fileSize = fileInfo.Length.ToString();

            // Prepare the JSON data
            DocumentD jsonData = new DocumentD
            {
                NameFile = fileName,
                Size = fileSize,
                Owner = new StudentD()
            };

            jsonData.Owner = onSucces;

            StartCoroutine(UploadDocumentPropertiesCoroutine(jsonData, onSuccess, onFaild));
        }, message =>
        {
            MainView.OnDebugged(message);
        }));
    }

    /// <summary>
    /// Coroutine for uploading document properties to the server.
    /// Gathers file attributes, converts them to JSON, and sends a POST request.
    /// </summary>
    /// <param name="filePath">The file path of the document.</param>
    /// <param name="onDocumentIdReceived">Callback to handle the newly created document ID.</param>
    /// <returns>IEnumerator for coroutine functionality.</returns>
    public IEnumerator UploadDocumentPropertiesCoroutine(DocumentD jsonData, Action<string> onSuccess, Action<string> onFaild)
    {
        string json = MainHandler.ToJson(jsonData, true);
        Debug.Log($"JSON being sent: {json}");

        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);

        using (UnityWebRequest request = new UnityWebRequest(AllUrlStudent.createDocument, "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.timeout = 30;

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log($"Response: {request.downloadHandler.text}");

                try
                {
                    var response = JsonConvert.DeserializeObject<MainData<DocumentUploadResponse>>(request.downloadHandler.text);

                    if (response != null && response.DataRaw != null && response.DataRaw["data"] is JObject)
                    {
                        var document = response.DataRaw["data"].ToObject<DocumentUploadResponse>();
                        if (document != null && !string.IsNullOrEmpty(document._id))
                        {
                            Debug.Log($"Document successfully uploaded. ID: {document._id}");
                            Debug.Log($"Document successfully uploaded. ID: {document.Owner}");
                            onSuccess?.Invoke(document._id);
                        }
                        else
                        {
                            Debug.LogError("Response does not contain a valid document ID.");
                            onFaild?.Invoke("Response does not contain a valid document ID.");
                        }
                    }
                    else
                    {
                        Debug.LogError("Unexpected response format.");
                        onFaild?.Invoke("Unexpected response format.");
                    }
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Error parsing response: {ex.Message}");
                    onFaild?.Invoke("Failed to parse response.");
                }
            }
            else
            {
                Debug.LogError($"Request failed: {request.error}");
                onFaild?.Invoke($"Request failed: {request.error}");
            }
        }
    }

    #endregion

    #region -- Methods --

    public IEnumerator GetStudentByID(string id, Action<StudentD> onSuccess, Action<string> onFailed)
    {

        //set URL for system to search the item
        string json = AllUrlStudent.findStudentById + id;

        using (UnityWebRequest request = UnityWebRequest.Get(json))
        {
            yield return request.SendWebRequest();

            MainData<StudentD> response = TransferStringToStudentD(request.downloadHandler.text);

            if (request.result == UnityWebRequest.Result.Success)
                onSuccess?.Invoke(response.Data[0]);
            else
                onFailed?.Invoke(response.Message);
        }
    }

    public MainData<StudentD> TransferStringToStudentD(string response)
    {
        MainData<StudentD> mainData = JsonConvert.DeserializeObject<MainData<StudentD>>(response);
        mainData.Initialize();
        return mainData;
    }

    public string TransferDataToJson(DocumentD printerDoc)
    {
        return MainHandler.ToJson<DocumentD>(printerDoc);
    }

    public MainData<DocumentD> TransferObjectToData(string response)
    {
        MainData<DocumentD> mainData = JsonConvert.DeserializeObject<MainData<DocumentD>>(response);
        mainData.Initialize();
        return mainData;
    }

    public MainData<string> TransferStringToData(string response)
    {
        MainData<string> mainData = JsonConvert.DeserializeObject<MainData<string>>(response);
        mainData.Initialize();
        return mainData;
    }

    #endregion
}

/// <summary>
/// Class representing the server's response for document upload.
/// </summary>
[System.Serializable]
public class DocumentUploadResponse
{
    public string NameFile;
    public long Size;
    public StudentD Owner;
    public string _id;
    public string createdAt;
    public string updatedAt;
    public int __v;
}
