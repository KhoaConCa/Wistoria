using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDetailPrinterUpdateHandler
{
    IEnumerator UpdatePrinterData(PrinterD printer, Action<string> onSuccess, Action<string> onFailed);

    IEnumerator GetAllCampus(Action<List<CampusD>> onCampusFound, Action<string> onSuccess, Action<string> onFailed);
}

public interface IGetPrinterHandler
{
    IEnumerator GetAllPrinter(Action<PrinterD> onPrinterFound, Action<string> onSuccess, Action<string> onFailed);
    IEnumerator SearchPrinterByName(string printerName, Action<PrinterD> onPrinterFound, Action<string> onSuccess, Action<string> onFailed);
    IEnumerator SearchCampusByID(string campusId, Action<CampusD> onCampusFound, Action<string> onSuccess, Action<string> onFailed);
}

public interface IAddPrinterHandler
{
    IEnumerator AddNewPrinter(PrinterD printer, Action<string> onSuccess, Action<string> onFailed);
    IEnumerator GetAllCampus(Action<List<CampusD>> onCampusFound, Action<string> onSuccess, Action<string> onFailed);
}