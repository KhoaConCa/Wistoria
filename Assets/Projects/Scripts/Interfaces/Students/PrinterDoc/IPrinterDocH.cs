using System;

public interface IUploadPrinterDocHandler
{
    void UploadPrinterDoc(PrinterDocD printerDoc, Action<bool, string> onComplete);
}
