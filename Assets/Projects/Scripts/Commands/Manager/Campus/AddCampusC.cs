    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddCampusC : MonoBehaviour, IAddCampusCommand
{
    #region -- Implements --

    /// <summary>
    /// Switch UI
    /// </summary>
    public void ClickAddButton()
    {
        _transformUI.SetActiveCampusUI(_addCampus);
    }

    #endregion

    #region -- Methods --

    void Start()
    {
        GetTransformUI();
        addButton.onClick.AddListener(ClickAddButton);
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

    #endregion

    #region -- Fields --

    public Button addButton;

    [SerializeField] private GameObject _addCampus;

    private ITransformUI _transformUI;

    #endregion
}
