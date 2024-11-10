using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DetailCampusC : MonoBehaviour, ICampusDetail
{
    #region -- Implements -- 

    public void DisplayCampusDetails(ICampusCardData campus)
    {
        if (campus == null)
        {
            Debug.LogWarning("Campus data is null. Cannot display details.");
            return;
        }

        campusNameInput.text = campus.CampusName;
        campusRoomInput.text = campus.CampusRoom;
        campusID = campus.CampusID;
    }

    #endregion

    #region -- Methods -- 

    void Start()
    {
        SetComponentBasedOn(_baseTransform, _campusNameText, _campusRoomText);


    }

    private void AddComponentData()
    {
        if (_cardData == null)
        {
            _cardData = gameObject.AddComponent<CampusCardData>();
        }
        else
        {
            Debug.Log("The CampusCardData component already exists");
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="root"></param>
    /// <param name="name"></param>
    /// <param name="room"></param>
    private void SetComponentBasedOn(string root, string name, string room)
    {
        Transform based = transform.root.Find(root);

        if (based != null)
        {
            Transform _name = based.Find(name);
            Transform _room = based.Find(room);

            if (_name != null && _room != null)
            {
                AddComponentGameObject(_name, _room);
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="nameLocation"></param>
    /// <param name="roomLocation"></param>
    private void AddComponentGameObject(Transform nameLocation, Transform roomLocation)
    {
        campusNameInput = nameLocation.GetComponent<TextMeshProUGUI>();
        campusRoomInput = roomLocation.GetComponent<TextMeshProUGUI>();
    }

    #endregion

    #region -- Fields -- 

    private ICampusCardData _cardData;
    private IDetailUpdate _detailHandler;

    [SerializeField] private Button _saveButton;
    [SerializeField] private Button _deleteButton;

    public TextMeshProUGUI campusNameInput;
    public TextMeshProUGUI campusRoomInput;

    private readonly string _baseTransform = "/GUI/Monitor/Campus/DetailCampus";
    private readonly string _campusNameText = "Body/SearchCard/ItemField/Campus" +
        "/InputField (TMP)/Text Area/PlaceHolderCampus";
    private readonly string _campusRoomText = "Body/SearchCard/ItemField/Room" +
        "/InputField (TMP)/Text Area/PlaceHolderRoom";

    private string campusID;

    #endregion
}
