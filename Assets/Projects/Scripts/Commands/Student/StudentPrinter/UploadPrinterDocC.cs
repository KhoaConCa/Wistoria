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

        // Tách callback thành hai hàm riêng để xử lý thành công và thất bại
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

        // Gọi UploadPrinterDoc với đủ tham số
        StartCoroutine(_handler.UploadPrinterDoc(printerDoc, OnSuccess, OnFaild));
    }
}
