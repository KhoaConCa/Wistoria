using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

public class ModifyStoreC : MonoBehaviour, IModifyStoreCommand
{
    #region -- Implements --

    /// <summary>
    /// Switch form, transfer data when the store card was clicked
    /// </summary>
    public void ClickCardToModify()
    {
        _detail.DisplayStoreDetails(_cardData);

        _transformUI.SetActiveObjectUI(_targetObject);
    }

    #endregion

    #region -- Methods --

    void Start()
    {
        GetTransformUI();
        GetComponentDetail();

        GetCardComponent();
    }

    #region -- Get Component --

    private void GetTransformUI()
    {
        if (_transformUI == null)
            _transformUI = GameObject.FindWithTag("MainUI").GetComponent<UITransformV>();
        else
            Debug.Log("The UITransformV component already exiests");
    }

    /// <summary>
    /// Set event for prefab
    /// </summary>
    public void GetCardComponent()
    {
        try
        {
            if (_cardData == null)
                _cardData = gameObject.GetComponent<StoreCardData>();
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    #endregion

    #region -- Add Component --


    private void GetComponentDetail()
    {
        if (_detail == null)
        {
            Transform childObject = MainView.FindObjectsByTag(GameObject.FindWithTag("MainUI").transform, "EditUI");
            _targetObject = childObject.gameObject;
            _detail = _targetObject.GetComponent<DetailStoreC>();
        }
        else
        {
            Debug.Log("The DetailStoreC component already exiests");
        }
    }
    #endregion

    #endregion

    #region -- Fields --

    private ITransformUI _transformUI;
    private IStoreCardData _cardData;
    private IStoreDetailCommand _detail;

    private Button _clickCard;

    [SerializeField] private GameObject _targetObject;

    #endregion
}
