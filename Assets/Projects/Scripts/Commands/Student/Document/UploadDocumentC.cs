using UnityEngine;

public class UploadDocumentC : MonoBehaviour, IUploadDocumentCommand
{
    private IUploadDocumentHandler _handler;
    private string _documentPath;
    private System.Action<string> _onDocumentIdReceived;

    /// <summary>
    /// Initializes the handler and document path for the upload command.
    /// </summary>
    /// <param name="handler">The upload document handler to manage upload process.</param>
    /// <param name="documentPath">The path to the document file to be uploaded.</param>
    /// <param name="onDocumentIdReceived">Callback to handle the document ID after upload.</param>
    public void Initialize(IUploadDocumentHandler handler, string documentPath, System.Action<string> onDocumentIdReceived)
    {
        _handler = handler;
        _documentPath = documentPath;
        _onDocumentIdReceived = onDocumentIdReceived;
    }

    /// <summary>
    /// Executes the document upload if the handler and document path are properly initialized.
    /// </summary>
    public void Execute()
    {
        if (_handler != null && !string.IsNullOrEmpty(_documentPath))
        {
            _handler.UploadDocumentProperties(_documentPath, _onDocumentIdReceived);
        }
        else
        {
            Debug.LogError("Handler or document path not initialized.");
        }
    }
}
