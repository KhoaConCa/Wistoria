using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

#region -- Interfaces Description --
/// <summary>
/// Interface for executing document upload commands.
/// </summary>
#endregion
public interface IUploadDocumentCommand
{
    /// <summary>
    /// Executes the document upload command.
    /// </summary>
    void Execute();
}

#region -- Interface for Document Handler --
/// <summary>
/// Interface for handling document upload operations.
/// Manages document property upload and supports coroutine for asynchronous uploads.
/// </summary>
#endregion
public interface IUploadDocumentHandler
{
    /// <summary>
    /// Initiates the upload of document properties.
    /// </summary>
    /// <param name="filePath">The file path of the document to be uploaded.</param>
    void UploadDocumentProperties(string filePath);

    /// <summary>
    /// Coroutine for uploading document properties asynchronously.
    /// </summary>
    /// <param name="filePath">The file path of the document.</param>
    /// <returns>IEnumerator for coroutine functionality.</returns>
    IEnumerator UploadDocumentPropertiesCoroutine(string filePath);
}
