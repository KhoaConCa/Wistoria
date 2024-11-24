using UnityEngine;

public class UploadPrinterDocC : MonoBehaviour, IUploadPrinterDocCommand
{
    private UploadPrinterDocH _handler;

    public void Initialize(UploadPrinterDocH handler)
    {
        _handler = handler;
    }

    public void Execute(PrinterDocD printerDoc, System.Action<bool, string> onUploadComplete)
    {
        if (_handler == null)
        {
            Debug.LogError("UploadPrinterDocH is not initialized.");
            return;
        }

        StartCoroutine(_handler.UploadPrinterDoc(printerDoc, onUploadComplete));
    }
}
