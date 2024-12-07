using System;
using System.Collections;

public interface IUploadPrinterDocHandler
{
    void UploadPrinterDoc(PrinterDocD printerDoc, Action<bool, string> onComplete);
}

public interface ICreatePrinterDocHandler
{
    IEnumerator CreatePrinterDoc(PrinterDocDStudent printerDoc, Action<string> onSuccess, Action<string> onFailed);

    IEnumerator UpdatePaper(StudentD studentD, Action<string> onSuccess, Action<string> onFailed);
}
