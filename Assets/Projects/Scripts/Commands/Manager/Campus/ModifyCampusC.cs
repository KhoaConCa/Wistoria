using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModifyCampusC : MonoBehaviour, IModifyCampusCommand
{
    #region -- Implements --

    public void ClickCard()
    {

    }

    #endregion

    #region -- Methods --

    public void SetupButton(GameObject prefab)
    {
        clickCard = prefab.GetComponent<Button>();

        if (clickCard != null)
        {
            // Add the ClickCard listener to the button if not already added
            clickCard.onClick.AddListener(ClickCard);
            Debug.Log("Button setup complete.");
        }
        else
        {
            Debug.LogError("Button component not found on the prefab!");
        }
    }

    void Start()
    {
        clickCard.onClick.AddListener(ClickCard);
    }

    #endregion

    #region -- Fields --

    public Button clickCard;

    private IModifyCampusView _modifyCampusView;

    #endregion
}
