using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Unity.VisualScripting;


public class SetDataPrinterV : MonoBehaviour, IPrinterDataSetter
{
    #region -- Implements --

    /// <summary>
    /// Add componet to from prefab selected
    /// </summary>
    /// <param name="printerName">Location of Text Name Field</param>
    /// <param name="printerCampus">Location of Text Campus Field</param>
    /// <param name="printerRoom">Location of Text Room Field</param>
    public void AddComponentFromPrefab(Transform printerName, Transform printerCampus, Transform printerRoom)
    {
        _printerName = printerName.gameObject.GetComponent<TextMeshProUGUI>();
        _printerCampus = printerCampus.gameObject.GetComponent<TextMeshProUGUI>();
        _printerLocateAt = printerRoom.gameObject.GetComponent<TextMeshProUGUI>();
    }

    public void SetDataPrinterCard(PrinterD printer)
    {
        _printerName.text = printer.PrinterName;
        _printerCampus.text = printer.LocateAt.Name;
        _printerLocateAt.text = printer.LocateAt.Room;
    }

    #endregion

    #region -- Fields --

    [SerializeField] private TextMeshProUGUI _printerName;
    [SerializeField] private TextMeshProUGUI _printerCampus;
    [SerializeField] private TextMeshProUGUI _printerLocateAt;

    #endregion
}
