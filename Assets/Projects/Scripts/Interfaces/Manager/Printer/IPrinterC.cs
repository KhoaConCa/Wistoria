using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGetPrinterCommand
{
    void OnPrinterFound(PrinterD printer);
}

public interface IModifyPrinterCommand
{
    void ClickCardToModify();
}

public interface IPrinterDetailCommand
{
    void DisplayPrinterDetails(IPrinterCardData cardData);
}

public interface IAddPrinterCommand
{
    void ClickAddButton();
}
