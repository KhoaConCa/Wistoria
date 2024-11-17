using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
using Utilities;

public class SpawnPrinterV : MonoBehaviour, IPrinterViewSpawner
{
    #region -- Implements --

    /// <summary>
    /// Using addressable to create prefab
    /// </summary>
    /// <param name="printer">Data of printer</param>
    public void CreateCard(PrinterD printer)
    {
        MainHandler.ClearSpawnedPrefabs();

        MainHandler.SpawnPrefabByLabel(_printerPrefab, _objectContain, (spawnedPrefab) =>
        {
            if (spawnedPrefab != null)
            {
                IPrinterCardData _printercardData = spawnedPrefab.GetComponent<PrinterCardData>();
                _printercardData.Initialize(printer);

                FindComponentUI();
                UpdateData(printer);
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
        AddComponentDefault();
        GetComponentDefault();
    }

    private void AddComponentDefault()
    {
        try
        {
            if (_setDataPrinterView == null)
                _setDataPrinterView = gameObject.AddComponent<SetDataPrinterV>();
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    private void GetComponentDefault()
    {
        try
        {
            if (_printerPrefab == null)
                _printerPrefab = new AssetLabelReference { labelString = "Printer" };

            if (_objectContain == null)
                _objectContain = GameObject.FindWithTag("ObjectContain");
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    private void FindComponentUI()
    {
        try
        {
            Transform positionPrinter = MainHandler.FindChildObjectsByTag(MainHandler.LastSpawnedPrefab.transform, _tagPrinter);
            Transform positionCampus = MainHandler.FindChildObjectsByTag(MainHandler.LastSpawnedPrefab.transform, _tagCampus);
            Transform positionRoom = MainHandler.FindChildObjectsByTag(MainHandler.LastSpawnedPrefab.transform, _tagRoom);

            if (positionPrinter != null && positionRoom != null)
                _setDataPrinterView.AddComponentFromPrefab(positionPrinter, positionCampus, positionRoom);
            else
                Debug.LogError("UI components not found in prefab!");
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    /// <summary>
    /// Set data for prefab
    /// </summary>
    /// <param name="name">Printer name</param>
    /// <param name="room">Printer room</param>
    private void UpdateData(PrinterD printer)
    {
        _setDataPrinterView.SetDataPrinterCard(printer);
    }

    #endregion

    #region -- Fields --

    private IPrinterDataSetter _setDataPrinterView;

    private GameObject _objectContain;

    [SerializeField] private AssetLabelReference _printerPrefab;
    private readonly string _tagPrinter = "ValueNamePrinter";
    private readonly string _tagCampus = "ValueCampus";
    private readonly string _tagRoom = "ValueRoom";

    private string _printerID;

    #endregion
}
