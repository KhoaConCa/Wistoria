using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class PrinterDocC : MonoBehaviour, IPrinterDocCommand
{
    #region -- Implements --

    public void CreatePrinterDoc(PrinterDocDStudent printerDoc)
    {
        _printerDoc = printerDoc;

        _paperNeed = CalculatePaper();
        if (_printerDoc.Document.Student.Paper < _paperNeed)
        {
            _printerDocV.TopUpPaper(_paperNeed);
            return;
        }

        _printerDoc.Process = PrinterDocStatus.In_Progress.ToString().Replace("_", " ");
        _printerDoc.PrinterRaw = printerDoc.Printer._id;
        _printerDoc.DocumentRaw = printerDoc.Document.Id;
        CreatePrinterDoc();
    }

    #endregion

    #region -- Methods --

    private void Start()
    {
        AddComponent();
        GetComponent();
    }

    private void AddComponent()
    {
        if (_createPrinterDocH == null)
            _createPrinterDocH = gameObject.AddComponent<UploadPrinterDocH>();        
    }

    private void GetComponent()
    {
        if (_printerDocV == null)
            _printerDocV = gameObject.GetComponent<UIPrinterDocV>();
    }

    private int CalculatePaper()
    {
        if (_printerDoc == null)
            return -1;

        int paperNeed = ((_printerDoc.PageEnd - _printerDoc.PageBegin + 1) / _printerDoc.Side) * _printerDoc.Copies;
        return paperNeed;
    }

    private void CreatePrinterDoc()
    {
        StartCoroutine(_createPrinterDocH.CreatePrinterDoc(_printerDoc, onSuccess =>
        {
            MainView.OnDebugged(onSuccess);

            UpdatePaperStudent();

        }, onFailed =>
        {
            MainView.OnReset(CreatePrinterDoc, onFailed);
        }));
    }

    private void UpdatePaperStudent()
    {
        StudentD updateStudent = _printerDoc.Document.Student;
        updateStudent.Paper -= _paperNeed;

        StartCoroutine(_createPrinterDocH.UpdatePaper(updateStudent, onSuccess =>
        {
            MainView.OnDebugged(onSuccess);

            _printerDocV.SwitchHistory();

        }, onFailed =>
        {
            MainView.OnReset(UpdatePaperStudent, onFailed);
        }));
    }

    #endregion

    #region -- Fields --

    private ICreatePrinterDocHandler _createPrinterDocH;
    private IPrinterDocView _printerDocV;

    private PrinterDocDStudent _printerDoc;
    private int _paperNeed;

    #endregion
}
