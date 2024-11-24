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
    /// Uploads document properties and handles the response for the document ID.
    /// </summary>
    /// <param name="filePath">The file path of the document.</param>
    /// <param name="onDocumentIdReceived">Callback for handling the document ID.</param>
    void UploadDocumentProperties(string filePath, System.Action<string> onDocumentIdReceived);

    /// <summary>
    /// Coroutine for uploading document properties asynchronously.
    /// </summary>
    /// <param name="filePath">The file path of the document.</param>
    /// <param name="onDocumentIdReceived">Callback for handling the document ID.</param>
    /// <returns>IEnumerator for coroutine functionality.</returns>
    IEnumerator UploadDocumentPropertiesCoroutine(string filePath, System.Action<string> onDocumentIdReceived);
}
