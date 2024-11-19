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

        Debug.Log($"Printer Clicked! Printer ID: {_printerDocData.PrinterId}");

        // Use the PrinterID for further operations
        string printerId = _printerDocData.PrinterId;

        // Example: You can pass this PrinterID to another handler or process
        ProcessPrinterID(printerId);
    }

    private void ProcessPrinterID(string printerId)
    {
        Debug.Log($"Processing Printer ID: {printerId}");
        // Implement additional logic here
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
        /*InitializeDependencies();
*/
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

/*    private void InitializeDependencies()
    {
        _paymentProcessor = gameObject.AddComponent<PaymentProcessor>();
        _studentUpdater = gameObject.AddComponent<StudentUpdater>();
    }*/

/*    public PackageD package;
*/    public Button clickStudentPrinter;

    private IPrinterDocData _printerDocData;
/*    private IPaymentProcessor _paymentProcessor;
    private IStudentUpdater _studentUpdater;*/
}
