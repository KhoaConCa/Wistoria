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
            Debug.Log($"Found printer: {studentPrinter.PrinterName}, campus: {studentPrinter.LocateAt.CampusName}, status: {studentPrinter.Status}");
            _spawnStudentPrinterView.CreateCard(studentPrinter);


        }
        else
        {
            Debug.Log("Package not found.");
        }
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
