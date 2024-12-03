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
        GameObject container = GameObject.FindWithTag(_path);

        MainHandler.SpawnPrefabByLabel(_studentPrinterPrefab, container, (spawnedPrefab) =>
        {
            if (spawnedPrefab == null)
            {
                Debug.LogError("Failed to spawn printer prefab!");
                return;
            }

            Debug.Log($"Prefab for {studentPrinter.PrinterName} spawned successfully.");

            var printerDocCardData = spawnedPrefab.GetComponent<PrinterDocCardData>();
            if (printerDocCardData == null)
            {
                Debug.LogError("PrinterDocCardData component is missing on the prefab.");
                return;
            }

            printerDocCardData.Initialize(studentPrinter._id, printerDoc);

            FindComponentUI();
            UpdateData(studentPrinter.PrinterName, studentPrinter.LocateAt?.Name ?? "Unknown", studentPrinter.Status);
        });
    }


    #endregion

    #region -- Methods --

    void Start()
    {
        _studentPrinterPrefab = new AssetLabelReference { labelString = "StudentPrinter" };

        AddComponentSetData();
    }

    private void FindComponentUI()
    {
        Transform lastestPrefab = MainHandler.LastSpawnedPrefab.transform;
        Transform positionPrinterName = MainView.FindObjectsByTag(lastestPrefab, _printerName);
        Transform positionCampusName = MainView.FindObjectsByTag(lastestPrefab, _campusName);
        Transform postionStatus = MainView.FindObjectsByTag(lastestPrefab, _printerStatus);

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

    private readonly string _path = "ObjectContain";
    private readonly string _printerName = "ValueName";
    private readonly string _campusName = "ValueCampus";
    private readonly string _printerStatus = "ValueStatus";

    #endregion
}
