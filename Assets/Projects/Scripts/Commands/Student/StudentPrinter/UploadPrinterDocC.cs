using UnityEngine;

public class UploadPrinterDocC : MonoBehaviour, IUploadPrinterDocCommand
{
    #region -- Implements --

    public void Execute(PrinterDocD printerDoc, System.Action<bool, string> onUploadComplete)
    {
        if (_handler == null)
        {
            Debug.LogError("UploadPrinterDocH is not initialized.");
            return;
        }

        void OnSuccess(string successMessage)
        {
            Debug.Log($"Upload successful: {successMessage}");
            onUploadComplete?.Invoke(true, successMessage);
        }

        void OnFaild(string errorMessage)
        {
            Debug.LogError($"Upload failed: {errorMessage}");
            onUploadComplete?.Invoke(false, errorMessage);
        }

        StartCoroutine(_handler.UploadPrinterDoc(printerDoc, OnSuccess, OnFaild));
    }

    #endregion

    #region -- Methods --

    public void Initialize(UploadPrinterDocH handler)
    {
        _handler = handler;
    }

    #endregion

    #region -- Fields --

    private UploadPrinterDocH _handler;

    #endregion
}
