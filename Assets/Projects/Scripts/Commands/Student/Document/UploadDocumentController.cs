using UnityEngine;
using UnityEngine.UI;

#region -- Class Description --
/// <summary>
/// Controller class for managing document upload functionality.
/// Initializes upload command and assigns the upload button's click event.
/// </summary>
#endregion
public class UploadDocumentController : MonoBehaviour
{
    #region -- Unity Methods --

    /// <summary>
    /// Unity's Start method.
    /// Initializes the upload command and sets up the upload button click event.
    /// </summary>
    private void Start()
    {
        // Create and attach the upload document handler
        var handler = gameObject.AddComponent<UploadDocumentH>();

        // Example file path for testing (replace with actual file path in production)
        _selectedFilePath = Application.dataPath + "/Sample.txt";

        // Create and initialize the upload command with the handler, file path, and callback for document ID
        var uploadCommandComponent = gameObject.AddComponent<UploadDocumentC>();
        uploadCommandComponent.Initialize(handler, _selectedFilePath, OnDocumentUploaded);
        _uploadCommand = uploadCommandComponent;

        // Set up the upload button's click event listener
        if (uploadButton != null)
        {
            uploadButton.onClick.AddListener(OnUploadButtonClicked);
        }
        else
        {
            Debug.LogError("Upload button not assigned in the Inspector.");
        }
        if (string.IsNullOrEmpty(DocumentService.DocumentId))
        {
            Debug.LogWarning("Document ID is not set. Ensure the document is uploaded before fetching printer data.");
        }
    }

    #endregion

    #region -- Private Methods --

    /// <summary>
    /// Called when the upload button is clicked.
    /// Executes the upload command if a file path is selected.
    /// </summary>
    private void OnUploadButtonClicked()
    {
        if (!string.IsNullOrEmpty(_selectedFilePath))
        {
            // Ensure the GameObject hosting the handler is active
            if (!gameObject.activeSelf)
            {
                gameObject.SetActive(true);
            }

            // Execute the upload command
            _uploadCommand.Execute();
        }
        else
        {
            Debug.LogError("No file path selected for upload.");
        }
    }


    /// <summary>
    /// Callback invoked when the document upload is completed.
    /// Logs the newly created document ID or an error message if the upload fails.
    /// </summary>
    /// <param name="documentId">The document ID returned from the server.</param>
    private void OnDocumentUploaded(string documentId)
    {
        if (!string.IsNullOrEmpty(documentId))
        {
            Debug.Log($"Document uploaded successfully. Document ID: {documentId}");
            DocumentService.DocumentId = documentId; // Store documentId in the shared data store
        }
        else
        {
            Debug.LogError("Failed to upload document or retrieve document ID.");
        }
    }

    #endregion

    #region -- Fields --

    [Header("Upload Button")]
    /// <summary>
    /// Reference to the upload button (should be assigned in the Inspector).
    /// </summary>
    public Button uploadButton;

    private IUploadDocumentCommand _uploadCommand;
    private string _selectedFilePath;

    #endregion
}
