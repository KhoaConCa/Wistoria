using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using PrinterDataManager;

public class ModifyPrinterC : MonoBehaviour, IModifyPrinterCommand
{
    #region -- Implements --

    /// <summary>
    /// Switch form, transfer data when the printer card was clicked
    /// </summary>
    public void ClickCard()
    {
        SetCurrentData(_cardData);

        _detail.DisplayPrinterDetails(_cardData);

        //_transformUI.SetActiveObjectUI(_modifyTagName);
    }

    /// <summary>
    /// Set event for prefab
    /// </summary>
    public void SetupButton()
    {
        _clickCard = gameObject.GetComponent<Button>();
        _cardData = gameObject.GetComponent<PrinterCardData>();
    }

    #endregion

    #region -- Methods --

    void Start()
    {
        GetTransformUI();
        GetComponentData();
        AddComponentDetail();

        SetupButton();

        _clickCard.onClick.AddListener(ClickCard);
    }

    #region -- Add Component --
    private void GetTransformUI()
    {
        if (_transformUI == null)
            _transformUI = GameObject.FindWithTag("MainUIPrinter").GetComponent<UITransformV>();
        else
            Debug.Log("The UITransformV component already exiests");
    }

    private void GetComponentData()
    {
        if (_cardData == null)
        {
            _cardData = gameObject.GetComponent<PrinterCardData>();
        }
        else
        {
            Debug.Log("The ModifyPrinterH component already exiests");
        }
    }

    private void AddComponentDetail()
    {
        if (_detail == null)
        {
            GameObject parentObject = GameObject.FindWithTag("MainUIPrinter");
            GameObject childParent = parentObject.GetComponent<UITransformV>().FindTargetObjectByTag("DetailPrinter");
            _detail = childParent.GetComponent<DetailPrinterC>();
        }
        else
        {
            Debug.Log("The DetailPrinterC component already exiests");
        }
    }
    #endregion

    private void SetCurrentData(IPrinterCardData cardData)
    {
        PrinterManager.currentPrinterID = cardData.PrinterID;
        PrinterManager.currentPrinterName = cardData.PrinterName;
        PrinterManager.currentLocateAt = cardData.LocateAt;
    }

    #endregion

    #region -- Fields --

    private Action<string> onClickCallback;

    private ITransformUI _transformUI;
    private IPrinterCardData _cardData;
    private IPrinterDetailCommand _detail;

    private Button _clickCard;

    [SerializeField] private string _modifyTagName;

    #endregion
}
