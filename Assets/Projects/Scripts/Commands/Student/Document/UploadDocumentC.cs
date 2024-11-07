using System.Collections;
using UnityEngine;

public class UploadDocumentC : MonoBehaviour, IUploadDocumentCommand
{
    private IUploadDocumentHandler _handler;
    private string _documentPath;

    // Phương thức khởi tạo giá trị cho _handler và _documentPath
    public void Initialize(IUploadDocumentHandler handler, string documentPath)
    {
        _handler = handler;
        _documentPath = documentPath;
    }

    public void Execute()
    {
        if (_handler != null && !string.IsNullOrEmpty(_documentPath))
        {
            _handler.UploadDocument(_documentPath);
        }
        else
        {
            Debug.LogError("Handler or document path not initialized.");
        }
    }
}
