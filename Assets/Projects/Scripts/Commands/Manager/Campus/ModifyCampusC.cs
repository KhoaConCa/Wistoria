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

    #endregion

    #region -- Fields --

    public Button clickCard;
    public CampusD campus;

    private ICampusDataGetter _modifyCampusView;

    #endregion
}
