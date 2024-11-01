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
        _modifyCampusView.GetCampusData();
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
            _modifyCampusView = gameObject.GetComponent<ModifyCampusV>();
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

    #endregion

    #region -- Fields --

    public Button clickCard;

    private IModifyCampusView _modifyCampusView;

    #endregion
}
