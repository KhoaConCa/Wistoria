using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CampusCardData : MonoBehaviour, ICampusCardData
{
    #region -- Implements --

    public void Initialize(string id, string name, string room)
    {
        CampusID = id;
        CampusName = name;
        CampusRoom = room;
    }

    #region -- Properties --
    public string CampusID { get; set; }
    public string CampusName { get; set; }
    public string CampusRoom { get; set; }
    #endregion

    #endregion

    #region -- Methods --

    void Start()
    {
        _campusDetail = gameObject.GetComponent<DetailCampusC>();

        clickButton.onClick.AddListener(() => OnCardClicked());
    }

    private void OnCardClicked()
    {
        if (_campusDetail != null)
        {
            _campusDetail.DisplayCampusDetails(this);
        }
        else
        {
            Debug.LogWarning("DetailCampusC reference is missing.");
        }
    }

    #endregion

    #region -- Fields --

    public Button clickButton;

    private ICampusDetail _campusDetail;

    #endregion
}
