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
    public void UploadDocumentProperties(string filePath)
    {
        StartCoroutine(UploadDocumentPropertiesCoroutine(filePath));
    }

    #endregion

    #region -- Coroutines --

    /// <summary>
    /// Coroutine for uploading document properties to the server.
    /// Gathers file attributes, converts them to JSON, and sends a POST request.
    /// </summary>
    /// <param name="filePath">The file path of the document.</param>
    /// <returns>IEnumerator for coroutine functionality.</returns>
    public IEnumerator UploadDocumentPropertiesCoroutine(string filePath)
    {
        // Lấy thuộc tính của tệp
        FileInfo fileInfo = new FileInfo(filePath);
        string fileName = fileInfo.Name;
        string fileSize = fileInfo.Length.ToString(); 
        string owner = "671860901e0844975517030e"; 

       
        DocumentD jsonData = new DocumentD
        {
            NameFile = fileName,
            Size = fileSize,
            Owner = owner
        };

        
        string json = MainHandler.ToJson(jsonData, true);
        Debug.Log("JSON being sent: " + json); 

        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);

        using (UnityWebRequest request = new UnityWebRequest("https://server-wistoria-api.vercel.app/document/create", "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.timeout = 30; 

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Document properties uploaded successfully");
            }
            else
            {
                Debug.LogError("Failed to upload document properties: " + request.error);
            }
        }
    }

    #endregion
}
