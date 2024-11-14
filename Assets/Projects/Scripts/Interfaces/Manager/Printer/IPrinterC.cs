using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGetPrinterCommand
{
    void ClickFindButton();
    void OnPrinterFound(PrinterD Printer);
}

public interface IModifyPrinterCommand
{
    void ClickCard();
    void SetupButton();
}

public interface IPrinterDetailCommand
{
    void DisplayPrinterDetails(IPrinterCardData cardData);
}

public interface IAddPrinterCommand
{
    void ClickAddButton();
}
