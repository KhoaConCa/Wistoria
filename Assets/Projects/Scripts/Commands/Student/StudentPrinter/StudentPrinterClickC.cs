using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Utilities;

public class StudentPrinterClickC : MonoBehaviour, IStudentPrinterClickC
{
    #region -- Implements --

    /// <summary>
    /// Handles the package click event.
    /// </summary>
    public void ClickStudentPrinter()
    {
        if (_printerDocData == null)
        {
            Debug.LogWarning("PrinterDoc data is null. Cannot proceed with Click.");
            return;
        }

        // Create PrinterDoc object for upload
        PrinterDocD printerDoc = _printerDocData.PrinterDocDetails;
        // Serialize the PrinterDoc to JSON
        string printerDocJson = MainHandler.ToJson(printerDoc, true); // Assuming MainHandler.ToJson exists
        Debug.Log($"JSON prepared for upload: {printerDocJson}");

        // Execute upload command
        _uploadCommand.Execute(printerDoc, OnUploadComplete);
    }

    /// <summary>
    /// Sets up the button event listener.
    /// </summary>
    public void SetUpButton()
    {
        clickStudentPrinter = gameObject.GetComponent<Button>();
        _printerDocData = gameObject.GetComponent<PrinterDocCardData>();
    }
    #endregion

    #region -- Methods --

    private void Start()
    {
        GetComponentData();

        // Attach the click listener
        clickStudentPrinter.onClick.AddListener(ClickStudentPrinter);

        SetUpButton();
        var handler = gameObject.AddComponent<UploadPrinterDocH>();
        var uploadCommand = gameObject.AddComponent<UploadPrinterDocC>();
        uploadCommand.Initialize(handler);
        _uploadCommand = uploadCommand;
    }

    private void GetComponentData()
    {
        if (_printerDocData == null)
        {
            _printerDocData = gameObject.GetComponent<PrinterDocCardData>();
        }
        else
        {
            Debug.Log("The PackageData component already exists.");
        }
    }

    private void OnUploadComplete(bool success, string response)
    {
        if (success)
        {
            Debug.Log($"Upload successful: {response}");
        }
        else
        {
            Debug.LogError($"Upload failed: {response}");
        }
    }

    #endregion

    #region -- Fields --

    private IPrinterDocData _printerDocData;
    private IUploadPrinterDocCommand _uploadCommand;

    [SerializeField] private Button clickStudentPrinter;

    #endregion
}
