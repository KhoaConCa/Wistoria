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
        Debug.Log(campus.CampusName);
        Debug.Log(campus.Room);
    }

    #endregion

    #region -- Methods --

    void Start()
    {
        AddComponentModifyView();
        clickCard.onClick.AddListener(ClickCard);
    }

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
    /// 
    /// </summary>
    /// <param name="campus"></param>
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

    #endregion

    #region -- Fields --

    public Button clickCard;
    public CampusD campus;
    private ICampusDataGetter _modifyCampusView;

    #endregion
}
