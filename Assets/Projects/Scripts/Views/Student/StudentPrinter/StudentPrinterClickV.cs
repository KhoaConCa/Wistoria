using UnityEngine;
using Utilities;

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

        if (_printerDocCard.Queue != null && _printerDocCard.Queue.Id == _printerDocData.Queue.Id)
        {
            _printerDocCard.Queue = new QueueD();
            _printerDocCard.Queue.Printer = new PrinterD();
            return;
        }

        if (_printerDocData.Queue.SlotRemaining == 0)
        {
            MainView.OnFailed("Hàng chờ của máy in này đã đầy. Vui lòng chọn máy in khác!");
            _printerDocCard.Queue = new QueueD();
            _printerDocCard.Queue.Printer = new PrinterD();
            return;
        } 
            
        _printerDocCard.Queue = _printerDocData.Queue;

        _printerDocCard.Queue.FirstSlot = ProcessingJson.InitializaProperty<PrinterDocDStudent>(_printerDocCard.Queue.FirstSlotRaw);
        _printerDocCard.Queue.SecondSlot = ProcessingJson.InitializaProperty<PrinterDocDStudent>(_printerDocCard.Queue.SecondSlotRaw);
        _printerDocCard.Queue.ThirdSlot = ProcessingJson.InitializaProperty<PrinterDocDStudent>(_printerDocCard.Queue.ThirdSlotRaw);

        Debug.Log($"printer name: {_printerDocCard.Queue.Printer._id}");
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
