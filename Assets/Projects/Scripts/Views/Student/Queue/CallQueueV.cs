using UnityEngine;
using UnityEngine.UI;
using Utilities;

public class CallQueueV : MonoBehaviour, IQueueV
{
    #region -- Implements --

    public void CheckingQueue()
    {
        if (_newQueue == null) return;

        if (_newQueue.FirstSlotId != _printerDocCard.Id && _time >= _timeCallBack)
        {
            IsChecking = false;

            _queueC.CallBackQueue(_newQueue.Id);
            MainView.OnDebugged("Đã thực hiện gọi lại hàng đợi!");

            _callTime += _time;
            _time = 0f;
        } 
    }

    public void ProcessQueue()
    {
        if (_newQueue.FirstSlotId == _printerDocCard.Id && _time >= _timeProcess)
        {
            IsChecking = false;

            _queueC.ProcessQueue(_newQueue.Id, 1);

            PrinterDocDStudent printerDoc = new PrinterDocDStudent();
            printerDoc.Initialize(_printerDocCard);

            _queueC.UpdatePrinterDoc(printerDoc, true);

            _callTime = 0f;
            _time = 0f;
        }
    }

    public void TimeOutQueue()
    {
        if (_callTime >= _timeoOut)
        {
            IsChecking = false;

            _callTime = 0f;
            _time = 0f;

            string message = "Đã hết thời gian chờ! Bạn có muốn tiếp tục chờ?";
            MainView.OnNotice(message, OnStop, OnContinue);
        }
    }

    public void SetNewQueue(QueueD queue)
    {
        _newQueue = queue;
    }

    #region -- Properties --
    public bool IsChecking { get; set; } = false;
    #endregion

    #endregion

    #region -- Methods --

    private void Awake()
    {
        AddComponent();
        GetComponent();
        AddQueue();

        Debug.Log(_printerDocCard.Document.Student.Paper);
    }

    private void Update()
    {
        if (!IsChecking) return;

        ProcessQueue();
        CheckingQueue();
        TimeOutQueue();

        _time += Time.deltaTime;
    }

    #region -- Get Component --
    private void GetComponent()
    {
        if (_printerDocCard == null)
            _printerDocCard = this.gameObject.GetComponent<PrinterDocCard>();
    }
    #endregion

    #region -- Add Component --
    private void AddComponent()
    {
        if (_queueC == null)
            _queueC = this.gameObject.AddComponent<CallQueueC>();
    }
    #endregion

    #region -- Add Queue --
    private void AddQueue()
    {
        _queueC.AddQueue(_printerDocCard.Queue, _printerDocCard.Id);
    }
    #endregion

    #region -- Time Out Queue --
    private void OnContinue()
    {
        IsChecking = true;
    }

    private void OnStop()
    {
        GameObject printButton = GameObject.FindWithTag("PrintButton");
        Button print = printButton.GetComponent<Button>();

        int queueIndex = 0;

        if (_newQueue.FirstSlotId == _printerDocCard.Id) queueIndex = 1;
        else if (_newQueue.SecondSlotId == _printerDocCard.Id) queueIndex = 2;
        else queueIndex = 3;

        _queueC.ProcessQueue(_newQueue.Id, queueIndex);

        CalculatePaper();

        PrinterDocDStudent printerDoc = new PrinterDocDStudent();
        printerDoc.Initialize(_printerDocCard);

        _queueC.UpdatePrinterDoc(printerDoc);

        print.onClick.Invoke();
    }
    #endregion

    #region -- Calculate Paper --
    private void CalculatePaper()
    {
        int paperNeed = ((_printerDocCard.PageEnd - _printerDocCard.PageBegin + 1) 
            / _printerDocCard.Side) * _printerDocCard.Copies;

        _printerDocCard.Document.Student.Paper += paperNeed;
    }
    #endregion

    #endregion

    #region -- Fields --

    private IQueueC _queueC;

    private PrinterDocCard _printerDocCard;
    private QueueD _newQueue = new QueueD();

    private const float _timeProcess = 4f;
    private const float _timeCallBack = 8f;
    private const float _timeoOut = 16f;

    private float _callTime = 0f;
    private float _time = 0f;

    #endregion
}
