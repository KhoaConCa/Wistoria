using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DetailCardCampusC : MonoBehaviour
{
    

    #region -- Methods --

    void Start()
    {
        SetAsDefault();
    }

    private void SetAsDefault()
    {
        _campusName = GameObject.FindWithTag("ValueCampus1").GetComponent<TextMeshProUGUI>();
        _campusRoom = GameObject.FindWithTag("ValueRoom1").GetComponent<TextMeshProUGUI>();
    }

    #endregion

    #region -- Fields --

    [SerializeField] private TextMeshProUGUI _campusName;
    [SerializeField] private TextMeshProUGUI _campusRoom;

    #endregion
}