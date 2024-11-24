using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

#region -- Class Description --
/// <summary>
/// Command class responsible for fetching and displaying package data.
/// Initializes required components, starts package retrieval, and handles found packages.
/// </summary>
#endregion
public class GetStudentPrinterC : MonoBehaviour, IGetStudentPrinterCommand
{
    #region -- Unity Methods --

    /// <summary>
    /// Unity's Start method.
    /// Adds required components and initiates the coroutine to get all packages.
    /// </summary>
    void Start()
    {
        AddComponentPackageHandler();
        AddComponentPackageView();
        StartCoroutine(_studentPrinterHandler.GetAllStudentPrinter(OnStudentPrinterFound));

    }

    #endregion

    #region -- Package Handling Methods --

    /// <summary>
    /// Callback executed when a package is found.
    /// Displays package information in the console and creates a package card in the view.
    /// </summary>
    /// <param name="package">The package data found by the handler.</param>
    public void OnStudentPrinterFound(StudentPrinterD studentPrinter)
    {
        if (studentPrinter != null)
        {
            // Example of fetching PrinterDocD (adjust as per your data source)
            PrinterDocD printerDoc = FetchPrinterDocData(studentPrinter._id);

            if (printerDoc != null)
            {
                Debug.Log($"Found printer: {studentPrinter.PrinterName}, campus: {studentPrinter.LocateAt.CampusName}, status: {studentPrinter.Status}");
                _spawnStudentPrinterView.CreateCard(studentPrinter, printerDoc);
            }
            else
            {
                Debug.LogError($"PrinterDoc data not found for Printer ID: {studentPrinter._id}");
            }
        }
        else
        {
            Debug.Log("Student printer not found.");
        }
    }

    private PrinterDocD FetchPrinterDocData(string printerId)
    {
        string documentId = DocumentService.DocumentId; // Retrieve documentId from the shared data store

        // Replace this with the actual logic to fetch PrinterDocD data
        return new PrinterDocD
        {
            PrintIn = printerId,
            FileDocument = documentId,
            PaperSize = DocumentService.PaperSize,
            Orientation = DocumentService.Orientation,
            Side = int.TryParse(DocumentService.Side, out int sideValue) ? sideValue : 1, // Default to 1 if parsing fails
            PageBegin = 1,
            PageEnd = 20,
            Copies = 1,
            Color = "Color"
        };
    }
    #endregion

    #region -- Add Components --

    /// <summary>
    /// Adds the SpawnPackageV component if it has not already been added.
    /// </summary>
    void AddComponentPackageView()
    {
        if (_spawnStudentPrinterView == null)
        {
            _spawnStudentPrinterView = gameObject.AddComponent<SpawnStudentPrinterV>();
        }
        else
        {
            Debug.Log("SpawnPackageV component already exists.");
        }
    }

    /// <summary>
    /// Adds the GetPackageH component if it has not already been added.
    /// </summary>
    void AddComponentPackageHandler()
    {
        if (_studentPrinterHandler == null)
        {
            _studentPrinterHandler = gameObject.AddComponent<GetStudentPrinterH>();
        }
        else
        {
            Debug.Log("GetPackageH component already exists.");
        }
    }


    #endregion

    #region -- Fields --

    private IGetStudentPrinterHandler _studentPrinterHandler;
    private ISpawnStudentPrinterView _spawnStudentPrinterView;

    #endregion
}
