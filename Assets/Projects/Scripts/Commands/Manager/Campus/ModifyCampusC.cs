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
        _modifyCampusView.GetCampusData(campus);
        _transformUI.SetActiveCampusUI(modifyObject);
        _setDataCampus.SetCampusNameInput(campus.CampusName);
        _setDataCampus.SetCampusRoomInput(campus.Room);
        //StartCoroutine(_modifyCampusHandler.CampusInformation(campus, OnCampusFound));
        Debug.Log(campus.CampusName);
        Debug.Log(campus.Room);
    }

    #endregion

    #region -- Methods --

    void Start()
    {
        AddComponentModifyView();
        AddComponentModifyHandler();
        GetComponentSetinfo();

        GetParentGameObject();
        GetTransformUI();

        clickCard.onClick.AddListener(ClickCard);
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
    #endregion

    /// <summary>
    /// Set event for prefab
    /// </summary>
    /// <param name="prefab">Prefab need to set</param>
    public void SetupButton(GameObject prefab)
    {
        clickCard = prefab.GetComponent<Button>();

        if (clickCard != null)
        {
            clickCard.onClick.AddListener(ClickCard);
        }
        else
        {
            Debug.LogError("Button component not found on the prefab!");
        }
    }

    /// <summary>
    /// Found campus by name and room
    /// </summary>
    /// <param name="campus">Campus data</param>
    public void OnCampusFound(CampusD campus)
    {
        _modifyCampusView.GetCampusData(campus);

        if (campus != null)
        {
            Debug.Log("Campus Data saved");
        }
        else
        {
            Debug.Log("Cannot save data");
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
    private ISetDataCampusInfo _setDataCampus;

    #endregion
}
