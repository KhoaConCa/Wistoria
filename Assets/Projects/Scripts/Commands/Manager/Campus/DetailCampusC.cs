using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEditor.PackageManager.Requests;
using CampusDataManager;

public class DetailCampusC : MonoBehaviour, ICampusDetailCommand
{
    #region -- Implements -- 

    /// <summary>
    /// Display Detail Campus to the prefab
    /// </summary>
    /// <param name="campus">Data of campus was clicked</param>
    public void DisplayCampusDetails(ICampusCardData campus)
    {
        if (campus == null)
        {
            Debug.LogWarning("Campus data is null. Cannot display details.");
            return;
        }

        _campusNameInput.text = campus.CampusName;
        _campusRoomInput.text = campus.CampusRoom;
    }

    #endregion

    #region -- Methods -- 

    void Start()
    {
        SetComponentBasedOn(_baseTransform, _campusNameText, _campusRoomText);
        AddComponentHandler();

        _saveButton.onClick.AddListener(OnClickSaveButton);
        GetDataModify();
    }

    /// <summary>
    /// Add component for GameObject
    /// </summary>
    /// <param name="nameLocation">Transform name campus field</param>
    /// <param name="roomLocation">Transform name room field</param>
    private void AddComponentGameObject(Transform nameLocation, Transform roomLocation)
    {
        _campusNameInput = nameLocation.GetComponent<TextMeshProUGUI>();
        _campusRoomInput = roomLocation.GetComponent<TextMeshProUGUI>();
    }

    private void AddComponentHandler()
    {
        if (_updateHandler == null)
        {
            _updateHandler = gameObject.AddComponent<DetailCampusH>();
        }
        else
        {
            Debug.Log("The DetailCampusH component already exists");
        }
    }

    /// <summary>
    /// Get transform of father component
    /// </summary>
    /// <param name="root">Father root</param>
    /// <param name="name">Child field name</param>
    /// <param name="room">Child field room</param>
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

    private void OnClickSaveButton()
    {
        SetDataModify();
        GetDataModify();
        StartCoroutine(_updateHandler.UpdateCampusData(_campusData, OnSuccess, OnFailed));
    }

    /// <summary>
    /// Handlers the response from the server when update successfully
    /// </summary>
    /// <param name="campus">Campus data updated</param>
    public void OnSuccess(CampusD campus)
    {
        Debug.Log($"Updated Campus: {campus.CampusName}, Room: {campus.Room}");
    }

    /// <summary>
    /// Handlers the response from the server when update failed
    /// </summary>
    public void OnFailed(CampusD campus)
    {
        Debug.Log($"Can not update Campus! Try again!");
    }

    /// <summary>
    /// Set new campus data
    /// </summary>
    private void GetDataModify()
    {
        _campusData._id = CampusManager.currentCampusID;
        _campusData.CampusName = CampusManager.currentCampusName;
        _campusData.Room = CampusManager.currentCampusRoom;
        _campusData.__v = CampusManager.__v;
    }

    private void SetDataModify()
    {
        CampusManager.currentCampusName = campusNameField.text;
        CampusManager.currentCampusRoom = campusRoomField.text;
    }

    #endregion

    #region -- Fields -- 

    private CampusD _campusData = new CampusD();

    private ICampusCardData _cardData;
    private IDataTransferHandler _detailHandler;
    private IDetailUpdateHandler _updateHandler;

    [SerializeField] private Button _saveButton;
    [SerializeField] private Button _deleteButton;
    [SerializeField] private Button _backButton;

    [SerializeField] private TextMeshProUGUI _campusNameInput;
    [SerializeField] private TextMeshProUGUI _campusRoomInput;

    [SerializeField] private TMP_Text campusNameField;
    [SerializeField] private TMP_Text campusRoomField;

    private readonly string _baseTransform = "/GUI/Monitor/Campus/DetailCampus";
    private readonly string _campusNameText = "Body/SearchCard/ItemField/Campus" +
        "/InputField (TMP)/Text Area/PlaceHolderCampus";
    private readonly string _campusRoomText = "Body/SearchCard/ItemField/Room" +
        "/InputField (TMP)/Text Area/PlaceHolderRoom";



    #endregion
}
