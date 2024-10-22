using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GetCampusC : MonoBehaviour
{
    #region -- Methods --

    void Start()
    {
        if (_campusHandler == null)
        {
            _campusHandler = gameObject.AddComponent<GetCampusH>();
        }

        getButton.onClick.AddListener(ClickGetButton);
    }

    /// <summary>
    /// When clicking button, GET request will be sent to the server
    /// </summary>
    void ClickGetButton()
    {
        string campusName = GetSelectedCampusName();

        if (!string.IsNullOrEmpty(campusName))
        {
            StartCoroutine(_campusHandler.GetCampus(campusName, OnCampusFound)); // Gọi OnCampusFound khi có kết quả
        }
        else
        {
            Debug.Log("Campus name cannot be empty.");
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="campus"></param>
    private void OnCampusFound(CampusD campus)
    {
        if (campus != null)
        {
            Debug.Log($"Found Campus: {campus.CampusName}, Room: {campus.Room}");
            Debug.Log(campus._id);
            Debug.Log(campus.CampusName);
            Debug.Log(campus.Room);
            Debug.Log(campus.createdAt);
            Debug.Log(campus.updatedAt);
            Debug.Log(campus.__v);
        }
        else
        {
            Debug.Log("Campus not found.");
        }
    }

    /// <summary>
    /// Lấy tên của mục được chọn từ dropdown.
    /// </summary>
    /// <returns>Tên campus đã chọn.</returns>
    string GetSelectedCampusName()
    {
        int selectedIndex = findNameInput.value; // Lấy index của mục được chọn
        return findNameInput.options[selectedIndex].text; // Lấy nội dung của mục đã chọn
    }

    #endregion

    #region -- Fields --

    public TMP_Dropdown findNameInput;

    public Button getButton;

    private GetCampusH _campusHandler;

    private CampusD _campusData;

    #endregion
}
