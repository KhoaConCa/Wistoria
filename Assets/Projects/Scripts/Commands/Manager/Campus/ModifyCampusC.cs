using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager;
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
        if (campus == null)
        {
            Debug.LogWarning("Campus data is null. Cannot proceed with ClickCard.");
            return;
        }

        _detail.DisplayCampusDetails(_cardData);
        SetCurrentData(_cardData);

        _transformUI.SetActiveCampusUI(modifyObject);
    }

    /// <summary>
    /// Set event for prefab
    /// </summary>
    public void SetupButton()
    {
        clickCard = gameObject.GetComponent<Button>();
        _cardData = gameObject.GetComponent<CampusCardData>();
    }

    #endregion

    #region -- Methods --

    void Start()
    {
        GetParentGameObject();
        GetTransformUI();
        GetComponentData();
        AddComponentDetail();

        clickCard.onClick.AddListener(ClickCard);

        SetupButton();
    }

    #region -- Add Component --
    private void GetTransformUI()
    {
        if (_transformUI == null)
        {
            _transformUI = gameObject.GetComponent<UITransformV>();
        }
        else
        {
            Debug.Log("The UITransformV component already exiests");
        }
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
            _detail = gameObject.AddComponent<DetailCampusC>();
        }
        else
        {
            Debug.Log("The DetailCampusC component already exiests");
        }
    }
    #endregion

    /// <summary>
    /// Get transform of DetailCampus GameObject even if it is inactive
    /// </summary>
    public void GetParentGameObject()
    {
        Transform campusTransform = transform.parent.parent.parent.parent.parent;

        if (campusTransform != null)
        {
            Transform detailCampusTransform = campusTransform.Find("DetailCampus");

            if (detailCampusTransform != null)
            {
                modifyObject = detailCampusTransform.gameObject;

                if (modifyObject == null)
                {
                    Debug.LogWarning("DetailCampus GameObject not found.");
                }
                else
                {
                    Debug.Log("Found DetailCampus GameObject, even if it is inactive.");
                }
            }
            else
            {
                Debug.LogWarning("DetailCampus transform not found in Campus hierarchy.");
            }
        }
        else
        {
            Debug.LogWarning("Campus transform not found.");
        }
    }

    private void SetCurrentData(ICampusCardData cardData)
    {
        CampusManager.currentCampusID = cardData.CampusID;
        CampusManager.currentCampusName = cardData.CampusName;
        CampusManager.currentCampusRoom = cardData.CampusRoom;
    }

    #endregion

    #region -- Fields --

    public Button clickCard;
    public CampusD campus;

    public GameObject modifyObject;

    private ITransformUI _transformUI;
    private ICampusCardData _cardData;
    private ICampusDetailCommand _detail;

    private Action<string> onClickCallback;

    #endregion
}
