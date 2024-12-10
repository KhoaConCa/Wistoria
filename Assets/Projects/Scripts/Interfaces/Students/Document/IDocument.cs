using System.Collections;
using System;

public interface IUploadDocumentCommand
{
    void Execute();

    void Initialize(IUploadDocumentHandler handler, string documentPath, System.Action<string> onDocumentIdReceived);
}

public interface IUploadDocumentHandler
{
    /// <summary>
    /// Initiates the coroutine to upload document properties.
    /// </summary>
    void UploadDocumentProperties(string filePath, Action<string> onSuccess, Action<string> onFaild);

    /// <summary>
    /// Coroutine for uploading document properties asynchronously.
    /// </summary>
    /// <param name="filePath">The file path of the document.</param>
    /// <param name="onDocumentIdReceived">Callback for handling the document ID.</param>
    /// <returns>IEnumerator for coroutine functionality.</returns>
    IEnumerator UploadDocumentPropertiesCoroutine(DocumentD jsonData, Action<string> onSuccess, Action<string> onFaild);
    IEnumerator UploadDocument(DocumentDStudent documentD, Action<DocumentDStudent> onSuccess, Action<string> onFaild);

    public IEnumerator GetStudentByID(string id, Action<StudentD> onSuccess, Action<string> onFailed);
}
