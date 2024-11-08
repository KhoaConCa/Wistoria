using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CampusCardData : MonoBehaviour, ICampusCardData
{
    public string CampusID { get; set; }
    public string CampusName { get; set; }
    public string CampusRoom { get; set; }

    public Button clickButton;

    private ICampusDetail _campusDetail;

    void Start()
    {
        _campusDetail = gameObject.GetComponent<DetailCampusC>();

        clickButton.onClick.AddListener(() => OnCardClicked());
    }

    private void OnCardClicked()
    {
        if (_campusDetail != null)
        {
            // Truyền dữ liệu của campus vào DetailCampusC
            _campusDetail.DisplayCampusDetails(this);
        }
        else
        {
            Debug.LogWarning("DetailCampusC reference is missing.");
        }
    }

    public void Initialize(string id, string name, string room)
    {
        CampusID = id;
        CampusName = name;
        CampusRoom = room;
    }
}
