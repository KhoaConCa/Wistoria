using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


public class SetDataPrinterV : MonoBehaviour, IPrinterDataSetter
{
    #region -- Implements --

    /// <summary>
    /// Add componet to from prefab selected
    /// </summary>
    /// <param name="nameLocation">Location of Text Name Field</param>
    /// <param name="roomLocation">Location of Text Room Field</param>
    public void AddComponentFromPrefab(Transform nameLocation, Transform printerLocation, Transform locationAt)
    {
        _printerName = nameLocation.GetComponent<TextMeshProUGUI>();
        _printerCampus = printerLocation.GetComponent<TextMeshProUGUI>();
        _printerLocateAt = locationAt.GetComponent<TextMeshProUGUI>();
    }

    public void SetDataPrinterCard(PrinterD printer)
    {
        _printerName.text = printer.PrinterName;
        _printerCampus.text = printer.LocateAt.Name;
        _printerLocateAt.text = printer.LocateAt.Room;
    }

    #endregion

    #region -- Fields --

    private TextMeshProUGUI _printerName;
    private TextMeshProUGUI _printerCampus;
    private TextMeshProUGUI _printerLocateAt;

    #endregion
}
