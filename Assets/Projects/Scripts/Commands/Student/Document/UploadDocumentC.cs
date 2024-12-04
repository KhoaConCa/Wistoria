using UnityEngine;

public class UploadDocumentC : MonoBehaviour, IUploadDocumentCommand
{
    #region -- Implements --

    public void Initialize(IUploadDocumentHandler handler, string documentPath,
                                    System.Action<string> onDocumentIdReceived)
    {
        _handler = handler;
        _documentPath = documentPath;
        _onDocumentIdReceived = onDocumentIdReceived;
    }

    public void Execute()
    {
        if (_handler != null && !string.IsNullOrEmpty(_documentPath))
        {
            _handler.UploadDocumentProperties(
                _documentPath,
                onSuccess: (documentId) =>
                {
                    Debug.Log($"Document uploaded successfully. ID: {documentId}");
                    _onDocumentIdReceived?.Invoke(documentId);
                },
                onFaild: (errorMessage) =>
                {
                    Debug.LogError($"Failed to upload document. Error: {errorMessage}");
                }
            );
        }
        else
        {
            Debug.LogError("Handler or document path not initialized.");
        }
    }

    public IUploadDocumentHandler GetHandler()
    {
        return _handler;
    }

    #endregion

    #region -- Fields --

    private IUploadDocumentHandler _handler;
    private string _documentPath;
    private System.Action<string> _onDocumentIdReceived;

    #endregion
}


