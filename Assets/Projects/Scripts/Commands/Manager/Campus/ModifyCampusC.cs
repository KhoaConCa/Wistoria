using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using CampusDataManager;

public class ModifyCampusC : MonoBehaviour, IModifyCampusCommand
{
    #region -- Implements --

    /// <summary>
    /// Switch form, transfer data when the campus card was clicked
    /// </summary>
    public void ClickCard()
    {
        SetCurrentData(_cardData);

        _detail.DisplayCampusDetails(_cardData);

        _transformUI.SetActiveObjectUI(_modifyTagName);
    }

    /// <summary>
    /// Set event for prefab
    /// </summary>
    public void SetupButton()
    {
        _clickCard = gameObject.GetComponent<Button>();
        _cardData = gameObject.GetComponent<CampusCardData>();
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
            _transformUI = GameObject.FindWithTag("MainUI").GetComponent<UITransformV>();
        else
            Debug.Log("The UITransformV component already exiests");
    }

    private void GetComponentData()
    {
        if (_cardData == null)
        {
            _cardData = gameObject.GetComponent<CampusCardData>();
        }
        else
        {
            Debug.Log("The ModifyCampusH component already exiests");
        }
    }

    private void AddComponentDetail()
    {
        if (_detail == null)
        {
            GameObject parentObject = GameObject.FindWithTag("MainUI");
            GameObject childParent = parentObject.GetComponent<UITransformV>().FindTargetObjectByTag("EditUI");
            _detail = childParent.GetComponent<DetailCampusC>();
        }
        else
        {
            Debug.Log("The DetailCampusC component already exiests");
        }
    }
    #endregion

    private void SetCurrentData(ICampusCardData cardData)
    {
        CampusManager.currentCampusID = cardData.CampusID;
        CampusManager.currentCampusName = cardData.CampusName;
        CampusManager.currentCampusRoom = cardData.CampusRoom;
    }

    #endregion

    #region -- Fields --

    private Action<string> onClickCallback;

    private ITransformUI _transformUI;
    private ICampusCardData _cardData;
    private ICampusDetailCommand _detail;

    private Button _clickCard;

    [SerializeField] private string _modifyTagName;

    #endregion
}
