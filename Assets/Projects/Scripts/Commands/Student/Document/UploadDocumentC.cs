using UnityEngine;

public class UploadDocumentC : MonoBehaviour, IUploadDocumentCommand
{
    private IUploadDocumentHandler _handler;
    private string _documentPath;
    private System.Action<string> _onDocumentIdReceived;

    public void Initialize(IUploadDocumentHandler handler, string documentPath, System.Action<string> onDocumentIdReceived)
    {
        _handler = handler;
        _documentPath = documentPath;
        _onDocumentIdReceived = onDocumentIdReceived;
    }

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

    public IUploadDocumentHandler GetHandler()
    {
        return _handler;
    }
}


