using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StudentPrinterClickH : MonoBehaviour, IStudentPrinterClickH
{
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

        // Log the PrinterDocCardData details
        Debug.Log($"--- Printer Clicked ---");
        Debug.Log($"Printer ID: {_printerDocData.PrinterId}");
        Debug.Log($"Document ID: {_printerDocData.PrinterDocDetails.DocumentId}");
        Debug.Log($"Paper Size: {_printerDocData.PrinterDocDetails.PaperSize}");
        Debug.Log($"Orientation:{_printerDocData.PrinterDocDetails.Orientation}");
        Debug.Log($"Side: {_printerDocData.PrinterDocDetails.Side}");
        Debug.Log($"Page Begin: {_printerDocData.PrinterDocDetails.PageBegin}");
        Debug.Log($"Page End: {_printerDocData.PrinterDocDetails.PageEnd}");
        Debug.Log($"Copies: {_printerDocData.PrinterDocDetails.Copies}");
        Debug.Log($"Color: {_printerDocData.PrinterDocDetails.Color}");

        // Create PrinterDoc object for upload
        PrinterDocD printerDoc = _printerDocData.PrinterDocDetails;

        // Execute upload command
        _uploadCommand.Execute(printerDoc, OnUploadComplete);
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

    /// <summary>
    /// Sets up the button event listener.
    /// </summary>
    public void SetUpButton()
    {
        clickStudentPrinter = gameObject.GetComponent<Button>();
        _printerDocData = gameObject.GetComponent<PrinterDocCardData>();
    }

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

    private IPrinterDocData _printerDocData;
    private IUploadPrinterDocCommand _uploadCommand;

    public Button clickStudentPrinter;
}
