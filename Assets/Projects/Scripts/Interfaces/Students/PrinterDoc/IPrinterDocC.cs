public interface IUploadPrinterDocCommand
{
    void Execute(PrinterDocD printerDoc, System.Action<bool, string> onUploadComplete);
}
