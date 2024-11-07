using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class UploadDocumentH : IUploadDocumentHandler
{
    // Xóa bỏ tham chiếu đến IUploadDocumentView

    public void UploadDocument(string filePath)
    {
        // Thực hiện tải tệp lên mà không cần hiển thị UI
        CoroutineHandler.StartStaticCoroutine(UploadDocumentCoroutine(filePath));
    }

    private IEnumerator UploadDocumentCoroutine(string filePath)
    {
        byte[] fileData = System.IO.File.ReadAllBytes(filePath);
        string fileName = System.IO.Path.GetFileName(filePath);

        WWWForm form = new WWWForm();
        form.AddBinaryData("file", fileData, fileName);

        using (UnityWebRequest request = UnityWebRequest.Post("https://example.com/upload", form))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Upload successful");
            }
            else
            {
                Debug.LogError("Upload failed: " + request.error);
            }
        }
    }
}
