using System;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using Utilities;

public class GetPrinterC : MonoBehaviour, IGetPrinterCommand
{
    #region -- Implements --

    /// <summary>
    /// Sends a GET request to the server when the button is clicked.
    /// Retrieves the printer information based on the selected printer name
    /// </summary>
    public void ClickFindButton()
    {
        string printerName = GetSelectedPrinterName();

        if (!string.IsNullOrEmpty(printerName))
        {
            StartCoroutine(_printerHandler.GetPrinter(printerName, OnPrinterFound));
        }
        else
        {
            Debug.Log("Printer name cannot be empty.");
        }
    }

    /// <summary>
    /// Handles the response from the server when printer information is found.
    /// Logs the details of the printer if it exists
    /// </summary>
    /// <param name="printer">The printer object returned from the server</param>
    public void OnPrinterFound(PrinterD printer)
    {
        if (printer != null)
            _spawnPrinterView.CreateCard(printer);
        else
            Debug.Log("Printer not found.");
    }

    #endregion

    #region -- Methods --

    private void Awake()
    {
        AddComponentPrinterHandler();
        AddComponetPrinterView();
        GetComponentUITransfer();

        getButton.onClick.AddListener(ClickFindButton);
        _addButton.onClick.AddListener(ClickAddButton);
    }

    private void OnEnable()
    {
        try
        {
            if (MainHandler.PrefabList.Count == 0)
            {
                StartCoroutine(_printerHandler.GetAllPrinter(OnPrinterFound));
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning(e.Message);
        }
    }

    private void OnDisable()
    {
        MainHandler.ClearSpawnedPrefabs();
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

    private void GetComponentUITransfer()
    {
        if (_transformUI == null)
            _transformUI = GameObject.FindWithTag("MainUIPrinter").GetComponent<UITransformV>();
        else
            Debug.Log("The UITransformV component already exists");
    }
    #endregion

    /// <summary>
    /// Retrieves the name of the selected printer from the dropdown list
    /// </summary>
    /// <returns>
    /// The name of the selected printer.
    /// Returns an empty string if no printer is selected
    /// </returns>
    public string GetSelectedPrinterName()
    {
        int selectedIndex = findNameInput.value;
        return findNameInput.options[selectedIndex].text;
    }

    private void ClickAddButton()
    {
        _transformUI.SetActiveObjectUI(_tagName);
    }

    #endregion

    #region -- Fields --

    private IGetPrinterHandler _printerHandler;
    private ITransformUI _transformUI;
    private IPrinterViewSpawner _spawnPrinterView;

    public Button getButton;
    [SerializeField] private Button _addButton;

    public TMP_Dropdown findNameInput;

    [TagSelector][SerializeField] private string _tagName;

    #endregion
}