using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class GetCampusC : MonoBehaviour, IGetCampusCommand
{
    #region -- Implements --

    /// <summary>
    /// Sends a GET request to the server when the button is clicked.
    /// Retrieves the campus information based on the selected campus name
    /// </summary>
    public void ClickFindButton()
    {
        string campusName = GetSelectedCampusName();

        if (!string.IsNullOrEmpty(campusName))
        {
            StartCoroutine(_campusHandler.GetCampus(campusName, OnCampusFound));
        }
        else
        {
            Debug.Log("Campus name cannot be empty.");
        }
    }

    /// <summary>
    /// Retrieves the name of the selected campus from the dropdown list
    /// </summary>
    /// <returns>
    /// The name of the selected campus.
    /// Returns an empty string if no campus is selected
    /// </returns>
    public string GetSelectedCampusName()
    {
        int selectedIndex = findNameInput.value;
        return findNameInput.options[selectedIndex].text;
    }

    /// <summary>
    /// Handles the response from the server when campus information is found.
    /// Logs the details of the campus if it exists
    /// </summary>
    /// <param name="campus">The campus object returned from the server</param>
    public void OnCampusFound(CampusD campus)
    {
        if (campus != null)
        {
            Debug.Log($"Found Campus: {campus.CampusName}, Room: {campus.Room}");
            SetCampusName(campus.CampusName);
            SetCampusRoom(campus.Room);
        }
        else
        {
            Debug.Log("Campus not found.");
        }
    }

    /// <summary>
    /// Get component and add listener
    /// </summary>
    public void Initialization()
    {
        if (_campusHandler == null)
        {
            _campusHandler = gameObject.AddComponent<GetCampusH>();
        }

        getButton.onClick.AddListener(ClickFindButton);
    }

    #endregion

    #region -- Methods --

    void Start()
    {
        Initialization();
    }

    void SetCampusName(string input)
    {
        campusName.text = input;
    }

    void SetCampusRoom(string input)
    {
        campusRoom.text = input;
    }

    #endregion

    #region -- Fields --

    public TextMeshProUGUI campusName;
    public TextMeshProUGUI campusRoom;

    public TMP_Dropdown findNameInput;

    public Button getButton;

    private IGetCampusHandler _campusHandler;

    #endregion
}