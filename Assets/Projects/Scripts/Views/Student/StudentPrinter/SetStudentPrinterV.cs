using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


public class SetStudentPrinterV : MonoBehaviour, ISetDataStudentPrinterView
{
    #region -- Implements --

    public void SetStudentPrinterPrinterName(string printerName)
    {
        studentPrinterPrinterName.text = printerName;
    }

    public void SetStudentPrinterCampusName(string campusName)
    {
        studentPrinterCampusName.text = campusName;
    }

    public void SetStudentPrinterStatus(string status)
    {
        studentPrinterStatus.text = status;
    } 
        

    #endregion

    #region -- Methods --

    /// <summary>
    /// 
    /// </summary>
    /// <param name="priceLocation"></param>
    /// <param name="paperLocation"></param>
    public void AddComponentFromPrefab(Transform printerNameLocation, Transform campusNameLocation, Transform statusLocation)
    {
        studentPrinterPrinterName = printerNameLocation.GetComponent<TextMeshProUGUI>();
        studentPrinterCampusName = campusNameLocation.GetComponent<TextMeshProUGUI>();
        studentPrinterStatus = statusLocation.GetComponent<TextMeshProUGUI>();
    }

    #endregion

    #region -- Fields --

    public TextMeshProUGUI studentPrinterPrinterName;
    public TextMeshProUGUI studentPrinterCampusName;
    public TextMeshProUGUI studentPrinterStatus;

    #endregion
}
