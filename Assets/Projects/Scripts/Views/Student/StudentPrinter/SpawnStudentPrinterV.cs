using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
using Utilities;

public class SpawnStudentPrinterV : MonoBehaviour, ISpawnStudentPrinterView
{
    #region -- Implements --

    /// <summary>
    /// Using addressable to create prefab
    /// </summary>
    /// <param name="package">Data of package</param>
    public void CreateCard(StudentPrinterD studentPrinter, PrinterDocD printerDoc)
    {
        MainHandler.ClearSpawnedPrefabs();

        MainHandler.SpawnPrefabByLabel(_studentPrinterPrefab, _path, (spawnedPrefab) =>
        {
            if (spawnedPrefab != null)
            {
                Debug.Log("Prefab spawned successfully.");

                var printerDocCardData = spawnedPrefab.GetComponent<PrinterDocCardData>();
                if (printerDocCardData != null)
                {
                    Debug.Log("PackageCardData component found. Initializing...");
                    printerDocCardData.Initialize(studentPrinter._id, printerDoc);
                }
                else
                {
                    Debug.LogError("PackageCardData component is missing on the prefab. Please ensure the component is attached.");
                }

                FindComponentUI(_studentPrinterPrinterName, _studentPrinterCampusName, _studentPrinterStatus);
                UpdateData(studentPrinter.PrinterName, studentPrinter.LocateAt.Name, studentPrinter.Status);
            }
            else
            {
                Debug.LogError("Failed to spawn prefab!");
            }
        });
    }


    #endregion

    #region -- Methods --

    void Start()
    {
        _studentPrinterPrefab = new AssetLabelReference { labelString = "StudentPrinter" };

        AddComponentSetData();
    }

    private void FindComponentUI(string printerName, string campusName, string status)
    {
        Transform positionPrinterName = MainHandler.LastSpawnedPrefab?.transform.Find(printerName);
        Transform positionCampusName = MainHandler.LastSpawnedPrefab?.transform.Find(campusName);
        Transform postionStatus = MainHandler.LastSpawnedPrefab?.transform.Find(status);

        if (positionPrinterName != null && positionCampusName != null && postionStatus != null)
        {
            _setDataStudentPrinterView.AddComponentFromPrefab(positionPrinterName, positionCampusName, postionStatus);
        }
        else
        {
            Debug.LogError("UI components not found in prefab!");
        }
    }

    private void AddComponentSetData()
    {
        if (_setDataStudentPrinterView == null)
        {
            _setDataStudentPrinterView = gameObject.AddComponent<SetStudentPrinterV>();
        }
        else
        {
            Debug.Log("The SetDataCampusV component already exists");
        }
    }


    private void UpdateData(string printerName, string campusName, string status)
    {
        _setDataStudentPrinterView.SetStudentPrinterPrinterName(printerName);
        _setDataStudentPrinterView.SetStudentPrinterCampusName(campusName);
        _setDataStudentPrinterView.SetStudentPrinterStatus(status);
    }

    #endregion

    #region -- Fields --

    private ISetDataStudentPrinterView _setDataStudentPrinterView;

    [SerializeField] private AssetLabelReference _studentPrinterPrefab;

    private readonly string _path = "/GUI/Body/Print/Feature/Printer/Printers/Contain";
    private readonly string _studentPrinterPrinterName = "Content/Text/Name";
    private readonly string _studentPrinterCampusName = "Content/Text/Campus";
    private readonly string _studentPrinterStatus = "Status/StatusTag_Active Variant/Label";

    #endregion
}
