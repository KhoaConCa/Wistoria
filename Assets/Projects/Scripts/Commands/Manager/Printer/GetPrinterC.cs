using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using Utilities;

public class GetPrinterC : MonoBehaviour, IGetPrinterCommand
{
    #region -- Implements --

    /// <summary>
    /// Handles the response from the server when printer information is found.
    /// Logs the details of the printer if it exists
    /// </summary>
    /// <param name="printer">The printer object returned from the server</param>
    public void OnPrinterFound(PrinterD printer)
    {
        if (printer == null)
        {
            Debug.Log("Printer not found.");
            return;
        }

        if (_printerIDFound.Contains(printer._id))
            return;



        _printerIDFound.Add(printer._id);
        if (!string.IsNullOrEmpty(printer.LocateAtID))
        {

            StartCoroutine(_printerHandler.SearchCampusByID(printer.LocateAtID, campus =>
            {
                if (campus != null)
                {
                    
                    printer.UpdateLocateAt(campus);
                    _spawnPrinterView.CreateCard(printer);
                }
            }, OnSuccess, OnFailed));
        }    
        else
            _spawnPrinterView.CreateCard(printer);
    }

    #endregion

    #region -- Methods --

    private void Awake()
    {
        AddComponentPrinterHandler();
        AddComponetPrinterView();
    }

    private void OnEnable()
    {
        try
        {
            MainHandler.ClearSpawnedPrefabs();
            _printerIDFound.Clear();

            if (MainHandler.PrefabList.Count <= 0 || _printerIDFound.Count <= 0)
                StartCoroutine(_printerHandler.GetAllPrinter(OnPrinterFound, OnSuccess, OnFailed));
            else
                Debug.Log("Can't spawn printer prefab: ");
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message); 
        }
    }

    #region -- Add Components --
    private void AddComponetPrinterView()
    {
        if (_spawnPrinterView == null)
        {
            _spawnPrinterView = gameObject.AddComponent<SpawnPrinterV>();
        }
        else
        {
            Debug.Log("The SpawnPrinterV component already exists");
        }
    }

    private void AddComponentPrinterHandler()
    {
        if (_printerHandler == null)
        {
            _printerHandler = gameObject.AddComponent<GetPrinterH>();

        }
        else
        {
            Debug.Log("The GetPrinterH component already exists");
        }
    }
    #endregion

    public void OnInputChange()
    {
        try
        {
            MainHandler.ClearSpawnedPrefabs();
            _printerIDFound.Clear();

            if (MainHandler.PrefabList.Count > 0 || _printerIDFound.Count > 0) return;

            if (_namePrinterField.text != "")
                StartCoroutine(_printerHandler.SearchPrinterByName(_namePrinterField.text, OnPrinterFound, OnSuccess, OnFailed));
            else
                StartCoroutine(_printerHandler.GetAllPrinter(OnPrinterFound, OnSuccess, OnFailed));
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    private void OnFailed(string log)
    {
        Debug.LogError(log);
        _noDataFound.SetActive(true);
    }

    private void OnSuccess(string log)
    {
        Debug.Log(log);
        _noDataFound.SetActive(false);
    }


    #endregion

    #region -- Fields --

    private IGetPrinterHandler _printerHandler;
    private IPrinterViewSpawner _spawnPrinterView;

    private static List<string> _printerIDFound = new List<string>();

    [SerializeField] private GameObject _noDataFound; 

    [SerializeField] private TMP_InputField _namePrinterField;

    #endregion
}