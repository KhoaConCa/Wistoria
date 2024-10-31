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

    void Start()
    {
        clickCard.onClick.AddListener(ClickCard);
    }

    #endregion

    #region -- Fields --

    public Button clickCard; 

    #endregion
}
