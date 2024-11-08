using UnityEngine;
using TMPro;

public class CampusDetailUI : Singleton<CampusDetailUI>
{
    [SerializeField] private TextMeshProUGUI campusNameText;
    [SerializeField] private TextMeshProUGUI roomText;

    /// <summary>
    /// Thiết lập chi tiết của Campus vào giao diện.
    /// </summary>
    public void SetCampusDetails(CampusD campus)
    {
        campusNameText.text = campus.CampusName;
        roomText.text = campus.Room;
    }
}
