using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Runtime.CompilerServices;


public interface IUploadDocumentCommand
{
    void Execute();
}

public interface IUploadDocumentHandler
{
    void UploadDocumentProperties(string filePath);
    IEnumerator UploadDocumentPropertiesCoroutine(string filePath);
}

/*public interface IUploadDocumentView
{
    void ShowLoading(bool isLoading);
    void ShowSuccess(string message);
    void ShowError(string message);
}*/


