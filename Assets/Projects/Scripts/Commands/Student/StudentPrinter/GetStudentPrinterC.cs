using System.Collections;
using System.Collections.Generic;
using System.Linq; // Sử dụng LINQ
using TMPro;
using UnityEngine;
using Utilities;

public class GetStudentPrinterC : MonoBehaviour, IGetStudentPrinterCommand
{
    private IGetStudentPrinterHandler _studentPrinterHandler;
    private ISpawnStudentPrinterView _spawnStudentPrinterView;

    [SerializeField] private TMP_Dropdown campusDropdown;
    private List<StudentPrinterD> allStudentPrinters;

    void Start()
    {
        AddComponentPackageHandler();
        AddComponentPackageView();

        allStudentPrinters = new List<StudentPrinterD>();

        campusDropdown.onValueChanged.AddListener(OnCampusSelected);

        StartCoroutine(_studentPrinterHandler.GetAllStudentPrinter(OnStudentPrinterFound));
    }

    public void OnStudentPrinterFound(StudentPrinterD studentPrinter)
    {
        if (studentPrinter != null)
        {
            allStudentPrinters.Add(studentPrinter);

            if (studentPrinter.Status == "Available")
            {
                PrinterDocD printerDoc = FetchPrinterDocData(studentPrinter._id);

                if (printerDoc != null)
                {
                    _spawnStudentPrinterView.CreateCard(studentPrinter, printerDoc);
                }
            }
        }

        UpdateCampusDropdown();
    }

    private void UpdateCampusDropdown()
    {
        List<string> campuses = allStudentPrinters
            .Select(printer => printer.LocateAt.Name)
            .Distinct()
            .ToList();

        campusDropdown.ClearOptions();
        campusDropdown.AddOptions(campuses);
    }

    private void OnCampusSelected(int index)
    {
        string selectedCampus = campusDropdown.options[index].text;

        List<StudentPrinterD> filteredPrinters = allStudentPrinters
            .Where(printer => printer.LocateAt.Name == selectedCampus && printer.Status == "Available")
            .ToList();

        DisplayFilteredPrinters(filteredPrinters);
    }

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

    private void AddComponentPackageView()
    {
        if (_spawnStudentPrinterView == null)
        {
            _spawnStudentPrinterView = gameObject.AddComponent<SpawnStudentPrinterV>();
        }
    }

    private void AddComponentPackageHandler()
    {
        if (_studentPrinterHandler == null)
        {
            _studentPrinterHandler = gameObject.AddComponent<GetStudentPrinterH>();
        }
    }
}
