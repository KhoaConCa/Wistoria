using UnityEngine;

public class StudentPrinterClickV : MonoBehaviour, IStudentPrinterClickV
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

        StudentPrinterD printer = _printerDocData.PrinterD;
        _printerDocCard.Printer = printer;

        Debug.Log($"printer name: {_printerDocCard.Printer.PrinterName}");
    }

    #endregion

    #region -- Methods --

    private void Start()
    {
        GetComponentData();
    }

    private void GetComponentData()
    {
        if (_printerDocData == null)
            _printerDocData = gameObject.GetComponent<PrinterDocCardData>();

        if (_printerDocCard == null)
        {
            GameObject printerDocCard = GameObject.FindWithTag("DefaultMainScene");
            _printerDocCard = printerDocCard.GetComponent<PrinterDocCard>();
        }
    }

    #endregion

    #region -- Fields --

    private IPrinterDocData _printerDocData;

    private PrinterDocCard _printerDocCard;

    #endregion
}
