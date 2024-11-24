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

        // Check and log Document ID
        if (string.IsNullOrEmpty(_printerDocData.PrinterDocDetails.DocumentId))
        {
            Debug.LogWarning("Document ID is null or empty. Ensure it is correctly assigned.");
        }
        else
        {
            Debug.Log($"Document ID: {_printerDocData.PrinterDocDetails.DocumentId}");
        }

        // Log other PrinterDoc details
        Debug.Log($"Paper Size: {_printerDocData.PrinterDocDetails.PaperSize}");
        Debug.Log($"Side: {_printerDocData.PrinterDocDetails.Side}");
        Debug.Log($"Page Begin: {_printerDocData.PrinterDocDetails.PageBegin}");
        Debug.Log($"Page End: {_printerDocData.PrinterDocDetails.PageEnd}");
        Debug.Log($"Copies: {_printerDocData.PrinterDocDetails.Copies}");
        Debug.Log($"Color: {_printerDocData.PrinterDocDetails.Color}");

        // Use the PrinterID for further operations
        string printerId = _printerDocData.PrinterId;

        // Example: You can pass this PrinterID to another handler or process
        ProcessPrinterID(printerId);
    }

    private void ProcessPrinterID(string printerId)
    {
        Debug.Log($"Processing Printer ID: {printerId}");
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

    public Button clickStudentPrinter;

    private IPrinterDocData _printerDocData;
}
