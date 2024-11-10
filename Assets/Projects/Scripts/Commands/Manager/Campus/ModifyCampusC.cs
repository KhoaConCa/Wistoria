using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ModifyCampusC : MonoBehaviour, IModifyCampusCommand
{
    #region -- Implements --

    public void ClickCard()
    {
        if (campus == null)
        {
            Debug.LogWarning("Campus data is null. Cannot proceed with ClickCard.");
            return;
        }

        _detail.DisplayCampusDetails(_cardData);

        _modifyCampusView.GetCampusData(campus);

        _transformUI.SetActiveCampusUI(modifyObject);

        Debug.Log(currentCampusID);
    }

    /// <summary>
    /// Set event for prefab
    /// </summary>
    public void SetupButton()
    {
        clickCard = gameObject.GetComponent<Button>();
        _cardData = gameObject.GetComponent<CampusCardData>();

        if (clickCard != null)
        {
            clickCard.onClick.AddListener(() =>
            {
                if (_cardData != null)
                {
                    currentCampusID = _cardData.CampusID;

                    ClickCard();
                }
                else
                {
                    Debug.LogWarning("CampusCardData is missing on the clicked prefab.");
                }
            });
        }
        else
        {
            Debug.LogError("Button component not found on the prefab!");
        }
    }


    public void GetCampusID(string id)
    {
        currentCampusID = id;
    }

    #endregion

    #region -- Methods --

    void Start()
    {
        AddComponentModifyView();
        AddComponentModifyHandler();
        GetComponentData();
        _detail = gameObject.AddComponent<DetailCampusC>();

        GetParentGameObject();
        GetTransformUI();
        SetupButton();
    }

    #region -- Add Component --
    private void AddComponentModifyView()
    {
        if (_modifyCampusView == null)
        {
            _modifyCampusView = gameObject.AddComponent<GetDataCampusV>();
        }
        else
        {
            Debug.Log("The ModifyCampusV component already exists");
        }
    }

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

    private void AddComponentModifyHandler()
    {
        if (_modifyCampusHandler == null)
        {
            _modifyCampusHandler = gameObject.AddComponent<ModifyCampusH>();
        }
        else
        {
            Debug.Log("The ModifyCampusH component already exiests");
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
    #endregion

    /// <summary>
    /// Found campus by name and room
    /// </summary>
    /// <param name="campus">Campus data</param>
    public void OnCampusFound(CampusD campus)
    {
        if (campus != null)
        {
            _modifyCampusView.GetCampusData(campus);
            Debug.Log("Campus Data saved");
        }
        else
        {
            Debug.LogWarning("Cannot save data. Campus is null.");
        }
    }

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

    #endregion

    #region -- Fields --

    public Button clickCard;
    public CampusD campus;

    public GameObject modifyObject;

    private ICampusDataGetter _modifyCampusView;
    private ITransformUI _transformUI;
    private IModifyCampusHandler _modifyCampusHandler;
    private ICampusCardData _cardData;
    private ICampusDetail _detail;

    public static string currentCampusID;
    private Action<string> onClickCallback;

    #endregion
}
