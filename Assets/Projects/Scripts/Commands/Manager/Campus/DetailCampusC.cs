using UnityEngine;
using TMPro; // Thêm thư viện nếu dùng TextMeshPro InputField
using UnityEngine.UI;

public class DetailCampusC : MonoBehaviour, ICampusDetail
{
    // Tham chiếu đến các InputField trong UI
    public TextMeshProUGUI campusNameInput;
    public TextMeshProUGUI campusRoomInput;

    void Start()
    {
        // Tìm đối tượng DetailCampus theo đường dẫn cụ thể
        Transform based = transform.root.Find("/GUI/Monitor/Campus/DetailCampus");

        if (based != null)
        {
            // Tìm các thành phần con với đường dẫn đầy đủ
            Transform name = based.Find("Body/SearchCard/ItemField/Campus/InputField (TMP)/Text Area/Placeholder");
            Transform room = based.Find("Body/SearchCard/ItemField/Room/InputField (TMP)/Text Area/Placeholder");

            if (name != null && room != null)
            {
                AddComponentGameObject(name, room);
            }
            else
            {
                Debug.LogWarning("CampusText or RoomText not found with specified path in DetailCampus.");
            }
        }
        else
        {
            Debug.LogError("DetailCampus not found in hierarchy.");
        }
    }


    public void AddComponentGameObject(Transform nameLocation, Transform roomLocation)
    {
        campusNameInput = nameLocation.GetComponent<TextMeshProUGUI>();
        campusRoomInput = roomLocation.GetComponent<TextMeshProUGUI>();
    }

    public void DisplayCampusDetails(ICampusCardData campus)
    {
        if (campus == null)
        {
            Debug.LogWarning("Campus data is null. Cannot display details.");
            return;
        }
        Debug.LogWarning("Campus data is null. Cannot display details.");
        // Gán dữ liệu vào các InputField
        campusNameInput.text = campus.CampusName;
        campusRoomInput.text = campus.CampusRoom;
    }
}
