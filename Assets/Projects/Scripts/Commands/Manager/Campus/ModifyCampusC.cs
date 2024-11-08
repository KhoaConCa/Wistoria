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
        Debug.Log(_currentCampusID);
        _transformUI.SetActiveCampusUI(modifyObject);

        //StartCoroutine(_modifyCampusHandler.CampusInformation(campus, OnCampusFound));
        Debug.Log(campus.CampusName);
        Debug.Log(campus.Room);
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
                    _currentCampusID = _cardData.CampusID;
/*                    campus = new CampusD
                    {
                        _id = _cardData.CampusID,
                        CampusName = _cardData.CampusName,
                        Room = _cardData.CampusRoom
                    };*/

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
        _currentCampusID = id;
    }

    #endregion

    #region -- Methods --

    void Start()
    {
        AddComponentModifyView();
        AddComponentModifyHandler();
        GetComponentSetinfo();
        GetComponentData();
        _detail = gameObject.AddComponent<DetailCampusC>();

        GetParentGameObject();
        GetTransformUI();
        _modifyCampusView.SetCampusID(_currentCampusID);
        SetupButton();

/*        if (clickCard != null)
        {
            clickCard.onClick.AddListener(ClickCard);
            Debug.Log(_currentCampusID);
        }
        else
        {
            Debug.LogWarning("clickCard is null. Make sure SetupButton is called with a valid button.");
        }*/
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

    private void GetComponentSetinfo()
    {
        if (_setDataCampus == null)
        {
            _setDataCampus = gameObject.GetComponent<SetInfoCampusV>();
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

    /// <summary>
    /// The function is called when the user clicks on a Campus item
    /// </summary>
    /// <param name="campusId">Campus ID is selected</param>
    public void OnCampusSelected(string campusId)
    {
        _currentCampusID = campusId;

        CampusD selectedCampus = CampusDataManager.Instance.GetCampusData(campusId);

        if (selectedCampus != null)
        {
            SetInfoCampusV.Instance.SetCampusNameInput(selectedCampus.CampusName);
            SetInfoCampusV.Instance.SetCampusRoomInput(selectedCampus.Room);
        }
        else
        {
            Debug.LogWarning("No data found for Campus with ID: " + campusId);
        }
    }

    public void UpdateCampusData()
    {
        if (string.IsNullOrEmpty(_currentCampusID))
        {
            Debug.LogWarning("Không có Campus nào được chọn để cập nhật.");
            return;
        }

        string updatedName = SetInfoCampusV.Instance.GetCampusNameInput();
        string updatedRoom = SetInfoCampusV.Instance.GetCampusRoomInput();

        CampusD updatedCampus = new CampusD
        {
            _id = _currentCampusID,
            CampusName = updatedName,
            Room = updatedRoom
        };

        StartCoroutine(_modifyCampusHandler.UpdateCampusData(updatedCampus, (result) =>
        {
            if (result != null)
            {
                Debug.Log("Cập nhật thành công Campus với ID: " + _currentCampusID);

                CampusDataManager.Instance.SetCampusData(result);
            }
            else
            {
                Debug.LogError("Cập nhật Campus thất bại.");
            }
        }));
    }

    private void OnCardClicked(string id)
    {
        onClickCallback?.Invoke(id);
    }

    #endregion

    #region -- Fields --

    public Button clickCard;
    public CampusD campus;

    public GameObject modifyObject;

    private ICampusDataGetter _modifyCampusView;
    private ITransformUI _transformUI;
    private IModifyCampusHandler _modifyCampusHandler;
    private ISetDataCampusInfo _setDataCampus;
    private ICampusCardData _cardData;
    private ICampusDetail _detail;

    private string _currentCampusID;
    private Action<string> onClickCallback;

    #endregion
}
