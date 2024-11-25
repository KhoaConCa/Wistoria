using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGetPrinterCommand
{
    void OnPrinterFound(PrinterD printer);
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
