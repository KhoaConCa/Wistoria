using UnityEngine;
using UnityEngine.UI;
using SimpleFileBrowser;

public class UploadDocumentController : MonoBehaviour
{
    #region -- Methods --

    private void Start()
    {
        GetComponent();

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

    private void GetComponent()
    {
        GameObject mainUI = GameObject.FindWithTag("MainUI");
        _transform = mainUI.GetComponent<UITransformV>();
    }

    private void OnUploadButtonClicked()
    {
        string selectedFilePath = "";
        FileBrowser.ShowLoadDialog(
            (paths) =>
            {
                selectedFilePath = paths[0];

                Debug.Log($"File selected: {selectedFilePath}");

                _uploadCommand.Initialize(_uploadCommand.GetHandler(), selectedFilePath, OnDocumentUploaded);
                _uploadCommand.Execute();

                _transform.SetActiveObjectUI(_targetObject);
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

    private ITransformUI _transform;
    private IUploadDocumentCommand _uploadCommand;

    [SerializeField] private Button uploadButton;

    [SerializeField] private GameObject _targetObject;

    #endregion
}
