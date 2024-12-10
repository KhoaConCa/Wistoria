using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TMPro;
using UnityEngine;
using Utilities;

/// <summary>
/// Command class responsible for managing and displaying student printers.
/// </summary>
public class GetStudentPrinterC : MonoBehaviour
{
    #region -- Methods --

    /// <summary>
    /// Unity Start method to initialize components and fetch data.
    /// </summary>
    void OnEnable()
    {
        InitializeComponents();

        _allQueue = new List<QueueD>();
        _campusName = new List<string>();

        campusDropdown.onValueChanged.AddListener(OnCampusSelected);

        GetStudentPrinter();
    }

    #region -- Initialize Component --
    /// <summary>
    /// Initializes required components.
    /// </summary>
    private void InitializeComponents()
    {
        if (_spawnStudentPrinterView == null)
        {
            _spawnStudentPrinterView = gameObject.AddComponent<SpawnStudentPrinterV>();
        }

        if (_studentPrinterHandler == null)
        {
            _studentPrinterHandler = gameObject.AddComponent<GetStudentPrinterH>();
        }
    }
    #endregion

    #region -- Get Printer --
    private void GetStudentPrinter()
    {
        StartCoroutine(_studentPrinterHandler.GetAllQueue(OnStudentPrinterFound, onSuccess =>
        {
            MainView.OnDebugged(onSuccess);
        }, onFailed =>
        {
            MainView.OnReset(GetStudentPrinter, onFailed);
        }));
    }

    /// <summary>
    /// Callback executed when a student printer is found.
    /// Adds printer to the list and updates the UI.
    /// </summary>
    /// <param name="studentPrinter">The printer data retrieved.</param>
    public void OnStudentPrinterFound(QueueD queue)
    {
        queue.Printer = ProcessingJson.InitializaProperty<PrinterD>(queue.PrinterRaw); 
        queue.Printer.ProcessLocateAt();

        if (queue.Printer != null && queue.Printer.Status == PrinterStatus.Available.ToString())
        {
            _allQueue.Add(queue);
            UpdateCampusDropdown(queue.Printer.LocateAt.Name);
            _spawnStudentPrinterView.CreateCard(queue);
        }
    }

    #endregion

    #region -- Update Data Drop Down --
    /// <summary>
    /// Updates the campus dropdown with unique campus names.
    /// </summary>
    private void UpdateCampusDropdown(string name)
    {
        if (!_campusName.Contains(name))
        {
            if (_campusName.Contains(_fixedItem))
                _campusName.Remove(_fixedItem);

            _campusName.Add(name);

            if (_campusName.Count >= 2)
                _campusName.Sort((x, y) => string.Compare(x, y, true, new CultureInfo("vi-VN")));

            _campusName.Insert(0, _fixedItem);

            campusDropdown.ClearOptions();
            campusDropdown.AddOptions(_campusName);
        }
    }
    #endregion

    #region -- Selcted Printer By Campus Name --
    /// <summary>
    /// Callback executed when a campus is selected in the dropdown.
    /// Filters printers by selected campus and displays them.
    /// </summary>
    /// <param name="index">The selected index of the dropdown.</param>
    private void OnCampusSelected(int index)
    {
        if (index == 0)
        {
            DisplayFilteredPrinters(_allQueue);
            return;
        }

        string selectedCampus = campusDropdown.options[index].text;

        // Filter printers by selected campus and status "Available"
        List<QueueD> filteredPrinters = _allQueue
            .Where(queue => queue.Printer.LocateAt.Name == selectedCampus)
            .ToList();
        
        DisplayFilteredPrinters(filteredPrinters);
    }

    /// <summary>
    /// Displays the filtered printers by spawning their prefabs.
    /// </summary>
    /// <param name="filteredPrinters">List of filtered printers.</param>
    private void DisplayFilteredPrinters(List<QueueD> filteredPrinters)
    {
        MainHandler.ClearSpawnedPrefabs();

        foreach (var printer in filteredPrinters)
        {
            _spawnStudentPrinterView.CreateCard(printer);
        }
    }
    #endregion

    #endregion

    #region -- Fields --

    private IGetStudentPrinterHandler _studentPrinterHandler;
    private ISpawnStudentPrinterView _spawnStudentPrinterView;

    [SerializeField] private TMP_Dropdown campusDropdown;

    private readonly string _fixedItem = "Tất cả";

    private List<QueueD> _allQueue;
    private List<string> _campusName;

    #endregion
}
