/*using UnityEngine;
using UnityEngine.UI;

public class DocumentView : MonoBehaviour, IUploadDocumentView
{
    public Text statusText;
    public Button uploadButton;

    public void ShowLoading(bool isLoading)
    {
        statusText.text = isLoading ? "Uploading..." : "Upload complete.";
        uploadButton.interactable = !isLoading;
    }

    public void ShowSuccess(string message)
    {
        statusText.text = message;
    }

    public void ShowError(string message)
    {
        statusText.text = message;
    }
}*/