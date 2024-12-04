using UnityEngine;
using UnityEngine.UI;
using SimpleFileBrowser;

public class UploadDocumentController : MonoBehaviour
{
    #region -- Methods --

    private void Start()
    {
        var handler = gameObject.AddComponent<UploadDocumentH>();
        var uploadCommandComponent = gameObject.AddComponent<UploadDocumentC>();
        uploadCommandComponent.Initialize(handler, null, OnDocumentUploaded);
        _uploadCommand = uploadCommandComponent;

        if (uploadButton != null)
        {
            uploadButton.onClick.AddListener(OnUploadButtonClicked);
        }
        else
        {
            Debug.LogError("Upload button not assigned in the Inspector.");
        }
    }

    private void OnUploadButtonClicked()
    {
        FileBrowser.ShowLoadDialog(
            (paths) =>
            {
                _selectedFilePath = paths[0];
                Debug.Log($"File selected: {_selectedFilePath}");
                _uploadCommand.Initialize(_uploadCommand.GetHandler(), _selectedFilePath, OnDocumentUploaded);
                _uploadCommand.Execute();
            },
            () => Debug.Log("File selection canceled."),
            FileBrowser.PickMode.Files,
            false,
            null,
            "*.pdf,*.doc,*.docx"
        );
    }

    private void OnDocumentUploaded(string documentId)
    {
        if (!string.IsNullOrEmpty(documentId))
        {
            Debug.Log($"Document uploaded successfully. Document ID: {documentId}");
            DocumentService.DocumentId = documentId;
        }
        else
        {
            Debug.LogError("Failed to upload document or retrieve document ID.");
        }
    }

    #endregion

    #region -- Fields --

    public Button uploadButton;

    private IUploadDocumentCommand _uploadCommand;

    private string _selectedFilePath;

    #endregion
}
