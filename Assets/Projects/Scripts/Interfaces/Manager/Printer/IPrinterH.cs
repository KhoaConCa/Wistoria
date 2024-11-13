using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataPrinterTransferHandler
{
    void TransferData(string response);
}

public interface IDetailPrinterUpdateHandler
{
    IEnumerator UpdatePrinterData(PrinterD Printer, Action<PrinterD> onSuccess, Action<PrinterD> onFailed);
}

public interface IGetPrinterHandler : IDataPrinterTransferHandler
{
    IEnumerator GetAllPrinter(Action<PrinterD> onPrinterFound);
    IEnumerator GetPrinter(string PrinterName, Action<PrinterD> onPrinterFound);
}

public interface IAddPrinterHandler
{
    IEnumerator AddNewPrinter(PrinterD Printer, Action<PrinterD> onSuccess);
}