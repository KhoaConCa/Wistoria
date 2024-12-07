using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq; // Sử dụng LINQ
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

        _allStudentPrinters = new List<StudentPrinterD>();
        _campusName = new List<string>() { "Tất cả" };

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
        StartCoroutine(_studentPrinterHandler.GetAllStudentPrinter(OnStudentPrinterFound, onSuccess =>
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
    public void OnStudentPrinterFound(StudentPrinterD printer)
    {
        if (printer != null && printer.Status == PrinterStatus.Available.ToString())
        {
            _allStudentPrinters.Add(printer);
            UpdateCampusDropdown(printer.LocateAt.Name);
            _spawnStudentPrinterView.CreateCard(printer);
        }
    }

    private void DisplayAvailablePrinters(List<StudentPrinterD> filteredPrinters)
    {
        MainHandler.ClearSpawnedPrefabs();

        foreach (var printer in filteredPrinters)
        {
            _spawnStudentPrinterView.CreateCard(printer);
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
            _campusName.Add(name);

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
            DisplayFilteredPrinters(_allStudentPrinters);
            return;
        }

        string selectedCampus = campusDropdown.options[index].text;

        // Filter printers by selected campus and status "Available"
        List<StudentPrinterD> filteredPrinters = _allStudentPrinters
            .Where(printer => printer.LocateAt.Name == selectedCampus)
            .ToList();
        
        DisplayFilteredPrinters(filteredPrinters);
    }

    /// <summary>
    /// Displays the filtered printers by spawning their prefabs.
    /// </summary>
    /// <param name="filteredPrinters">List of filtered printers.</param>
    private void DisplayFilteredPrinters(List<StudentPrinterD> filteredPrinters)
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

    private List<StudentPrinterD> _allStudentPrinters;
    private List<string> _campusName;

    #endregion
}
