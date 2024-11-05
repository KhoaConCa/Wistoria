using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddCampusC : MonoBehaviour, IAddCampusCommand
{
    #region -- Implements --

    public void ClickAddButton()
    {
        _transformUI.SetActiveCampusUI(addCampus);
    }

    #endregion

    #region -- Methods --

    void Start()
    {
        addButton.onClick.AddListener(ClickAddButton);
        _transformUI = gameObject.GetComponent<UITransformV>();
    }

    #endregion

    #region -- Fields --

    public Button addButton;

    [SerializeField] public GameObject addCampus;

    private ITransformUI _transformUI;

    #endregion
}
