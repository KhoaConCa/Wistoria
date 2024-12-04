using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Utilities;

public class GetStudentPrinterC : MonoBehaviour, IGetStudentPrinterCommand
{
    #region -- Implements --

    /// <summary>
    /// Initializes required components.
    /// </summary>
    public void InitializeComponents()
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

    #region -- Methods --

    /// <summary>
    /// Unity Start method to initialize components and fetch data.
    /// </summary>
    void Start()
    {
        InitializeComponents();

        _allStudentPrinters = new List<StudentPrinterD>();

        campusDropdown.onValueChanged.AddListener(OnCampusSelected);

        StartCoroutine(_studentPrinterHandler.GetAllStudentPrinter(
            OnStudentPrinterFound,
            OnFetchSuccess,
            OnFetchFailed
        ));
    }

    /// <summary>
    /// Callback executed when a student printer is found.
    /// Adds printer to the list and updates the UI.
    /// </summary>
    /// <param name="studentPrinter">The printer data retrieved.</param>
    public void OnStudentPrinterFound(StudentPrinterD studentPrinter)
    {
        if (studentPrinter != null)
        {
            _allStudentPrinters.Add(studentPrinter);

            // Chỉ xử lý các máy in có trạng thái Available
            if (studentPrinter.Status == "Available")
            {
                PrinterDocD printerDoc = FetchPrinterDocData(studentPrinter._id);
                if (printerDoc != null)
                {
                    _spawnStudentPrinterView.CreateCard(studentPrinter, printerDoc);
                }
                else
                {
                    Debug.LogWarning($"No document data found for printer: {studentPrinter.PrinterName}");
                }
            }
            else
            {
                Debug.Log($"Printer {studentPrinter.PrinterName} is not available. Skipping...");
            }
        }

        UpdateCampusDropdown();
    }

    private void DisplayAvailablePrinters(List<StudentPrinterD> filteredPrinters)
    {
        MainHandler.ClearSpawnedPrefabs();

        foreach (var printer in filteredPrinters)
        {
            // Chỉ hiển thị các máy in có trạng thái Available
            if (printer.Status == "Available")
            {
                PrinterDocD printerDoc = FetchPrinterDocData(printer._id);
                if (printerDoc != null)
                {
                    _spawnStudentPrinterView.CreateCard(printer, printerDoc);
                }
            }
            else
            {
                Debug.Log($"Skipping printer {printer.PrinterName} (Status: {printer.Status})");
            }
        }
    }


    /// <summary>
    /// Callback executed when fetching printers succeeds.
    /// </summary>
    /// <param name="message">Success message.</param>
    private void OnFetchSuccess(string message)
    {
        Debug.Log($"Fetch successful: {message}");
    }

    /// <summary>
    /// Callback executed when fetching printers fails.
    /// </summary>
    /// <param name="error">Error message.</param>
    private void OnFetchFailed(string error)
    {
        Debug.LogError($"Fetch failed: {error}");
    }

    /// <summary>
    /// Updates the campus dropdown with unique campus names.
    /// </summary>
    private void UpdateCampusDropdown()
    {
        List<string> campuses = _allStudentPrinters
            .Where(printer => printer.LocateAt != null)
            .Select(printer => printer.LocateAt.Name)
            .Distinct()
            .ToList();

        campusDropdown.ClearOptions();
        campusDropdown.AddOptions(campuses);

        Debug.Log("Updated campus dropdown with campuses: " + string.Join(", ", campuses));
    }

    /// <summary>
    /// Callback executed when a campus is selected in the dropdown.
    /// Filters printers by selected campus and displays them.
    /// </summary>
    /// <param name="index">The selected index of the dropdown.</param>
    private void OnCampusSelected(int index)
    {
        string selectedCampus = campusDropdown.options[index].text;

        List<StudentPrinterD> filteredPrinters = _allStudentPrinters
            .Where(printer => printer.LocateAt.Name == selectedCampus && printer.Status == "Available")
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
            PrinterDocD printerDoc = FetchPrinterDocData(printer._id);
            if (printerDoc != null)
            {
                _spawnStudentPrinterView.CreateCard(printer, printerDoc);
            }
        }
    }

    /// <summary>
    /// Fetches document data for a printer.
    /// </summary>
    /// <param name="printerId">The ID of the printer.</param>
    /// <returns>The printer document data.</returns>
    private PrinterDocD FetchPrinterDocData(string printerId)
    {
        string documentId = DocumentService.DocumentId;

        return new PrinterDocD
        {
            PrintIn = printerId,
            FileDocument = documentId,
            PaperSize = DocumentService.PaperSize,
            Orientation = DocumentService.Orientation,
            Side = int.TryParse(DocumentService.Side, out int sideValue) ? sideValue : 1,
            PageBegin = 1,
            PageEnd = 20,
            Copies = 1,
            Color = "Color"
        };
    }

    #endregion

    #region -- Fields --

    private IGetStudentPrinterHandler _studentPrinterHandler;
    private ISpawnStudentPrinterView _spawnStudentPrinterView;

    [SerializeField] private TMP_Dropdown campusDropdown;
    private List<StudentPrinterD> _allStudentPrinters;

    #endregion
}
