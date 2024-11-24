using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.IO;
using System.Text;
using Utilities;

#region -- Class Description --
/// <summary>
/// Handler class responsible for managing document property upload operations.
/// Prepares document data, serializes it to JSON, and sends it to the server.
/// </summary>
#endregion
public class UploadDocumentH : MonoBehaviour, IUploadDocumentHandler
{
    #region -- Public Methods --

    /// <summary>
    /// Initiates the coroutine to upload document properties.
    /// </summary>
    /// <param name="filePath">The file path of the document to be uploaded.</param>
    /// <param name="onDocumentIdReceived">Callback to handle the newly created document ID.</param>
    public void UploadDocumentProperties(string filePath, System.Action<string> onDocumentIdReceived)
    {
        if (!gameObject.activeSelf)
        {
            Debug.LogWarning("GameObject is inactive. Activating temporarily to start coroutine.");
            gameObject.SetActive(true);
        }

        StartCoroutine(UploadDocumentPropertiesCoroutine(filePath, onDocumentIdReceived));
    }


    #endregion

    #region -- Coroutines --

    /// <summary>
    /// Coroutine for uploading document properties to the server.
    /// Gathers file attributes, converts them to JSON, and sends a POST request.
    /// </summary>
    /// <param name="filePath">The file path of the document.</param>
    /// <param name="onDocumentIdReceived">Callback to handle the newly created document ID.</param>
    /// <returns>IEnumerator for coroutine functionality.</returns>
    public IEnumerator UploadDocumentPropertiesCoroutine(string filePath, System.Action<string> onDocumentIdReceived)
    {
        // Gather file attributes
        FileInfo fileInfo = new FileInfo(filePath);
        string fileName = fileInfo.Name;
        string fileSize = fileInfo.Length.ToString();
        string owner = "671860901e0844975517030e"; // Replace with the actual owner ID

        // Prepare the JSON data
        DocumentD jsonData = new DocumentD
        {
            NameFile = fileName,
            Size = fileSize,
            Owner = owner
        };

        // Serialize to JSON
        string json = MainHandler.ToJson(jsonData, true);
        Debug.Log("JSON being sent: " + json);

        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);

        using (UnityWebRequest request = new UnityWebRequest("https://server-wistoria-api.vercel.app/document/create", "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.timeout = 30; // Set a timeout for the request

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Document properties uploaded successfully");

                // Parse the response to extract the new _id
                string responseText = request.downloadHandler.text;
                Debug.Log("Server Response: " + responseText);

                // Deserialize the response to get the _id
                DocumentUploadResponse response = JsonUtility.FromJson<DocumentUploadResponse>(responseText);

                if (response != null && !string.IsNullOrEmpty(response._id))
                {
                    Debug.Log($"New Document ID: {response._id}");
                    onDocumentIdReceived?.Invoke(response._id); // Pass the _id to the callback
                }
                else
                {
                    Debug.LogError("Failed to parse the document ID from the response.");
                    onDocumentIdReceived?.Invoke(null);
                }
            }
            else
            {
                Debug.LogError("Failed to upload document properties: " + request.error);
                onDocumentIdReceived?.Invoke(null); // Pass null to indicate failure
            }
        }
    }


    #endregion
}

#region -- Response Class --
/// <summary>
/// Class representing the server's response for document upload.
/// </summary>
[System.Serializable]
public class DocumentUploadResponse
{
    public string NameFile;   // Maps to "NameFile"
    public long Size;         // Maps to "Size"
    public string Owner;      // Maps to "Owner"
    public string _id;        // Maps to "_id"
    public string createdAt;  // Maps to "createdAt"
    public string updatedAt;  // Maps to "updatedAt"
    public int __v;           // Maps to "__v"
}

#endregion
