using System.Collections;
using UnityEngine;

#region -- Class Description --
/// <summary>
/// Command class responsible for executing document upload through the handler.
/// Initializes the handler and document path required for uploading.
/// </summary>
#endregion
public class UploadDocumentC : MonoBehaviour, IUploadDocumentCommand
{
    #region -- Public Methods --

    /// <summary>
    /// Initializes the handler and document path for the upload command.
    /// </summary>
    /// <param name="handler">The upload document handler to manage upload process.</param>
    /// <param name="documentPath">The path to the document file to be uploaded.</param>
    public void Initialize(IUploadDocumentHandler handler, string documentPath)
    {
        _handler = handler;
        _documentPath = documentPath;
    }

    /// <summary>
    /// Executes the document upload if the handler and document path are properly initialized.
    /// </summary>
    public void Execute()
    {
        if (_handler != null && !string.IsNullOrEmpty(_documentPath))
        {
            _handler.UploadDocumentProperties(_documentPath);
        }
        else
        {
            Debug.LogError("Handler or document path not initialized.");
        }
    }

    #endregion

    #region -- Fields --

    private IUploadDocumentHandler _handler;
    private string _documentPath;

    #endregion
}
