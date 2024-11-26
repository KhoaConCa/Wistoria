using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using PrinterDataManager;
using Utilities;

public class ModifyPrinterC : MonoBehaviour, IModifyPrinterCommand
{
    #region -- Implements --

    /// <summary>
    /// Switch form, transfer data when the printer card was clicked
    /// </summary>
    public void ClickCard()
    {
        _detail.DisplayPrinterDetails(_cardData);

        _transformUI.SetActiveObjectUI(_targetObject);
    }

    /// <summary>
    /// Set event for prefab
    /// </summary>
    public void SetupButton()
    {
        try
        {
            if (_clickCard == null)
                _clickCard = gameObject.GetComponent<Button>();

            if (_cardData == null)
                _cardData = gameObject.GetComponent<PrinterCardData>();
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    #endregion

    #region -- Methods --

    void Start()
    {
        GetTransformUI();
        GetComponentDetail();

        SetupButton();

        _clickCard.onClick.AddListener(ClickCard);
    }

    #region -- Add Component --
    private void GetTransformUI()
    {
        if (_transformUI == null)
            _transformUI = GameObject.FindWithTag("MainUI").GetComponent<UITransformV>();
        else
            Debug.Log("The UITransformV component already exiests");
    }

    private void GetComponentDetail()
    {
        if (_detail == null)
        {
            Transform childObject = MainView.FindObjectsByTag(GameObject.FindWithTag("MainUI").transform, "EditUI");
            _targetObject = childObject.gameObject;
            _detail = _targetObject.GetComponent<DetailPrinterC>();
        }
        else
        {
            Debug.Log("The DetailPrinterC component already exiests");
        }
    }
    #endregion

    #endregion

    #region -- Fields --

    private ITransformUI _transformUI;
    private IPrinterCardData _cardData;
    private IPrinterDetailCommand _detail;

    private Button _clickCard;

    [SerializeField] private GameObject _targetObject;

    #endregion
}
