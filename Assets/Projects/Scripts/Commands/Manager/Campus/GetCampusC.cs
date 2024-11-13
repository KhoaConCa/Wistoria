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
    /// Handles the response from the server when campus information is found.
    /// Logs the details of the campus if it exists
    /// </summary>
    /// <param name="campus">The campus object returned from the server</param>
    public void OnCampusFound(CampusD campus)
    {
        if (campus != null)
        {
            Debug.Log($"Found Campus: {campus.CampusName}, Room: {campus.Room}");
            _SpawnCampusView.CreateCard(campus);
        }
        else
        {
            Debug.Log("Campus not found.");
        }
    }

    #endregion

    #region -- Methods --

    void Start()
    {
        AddComponentCampusHandler();
        AddComponetCampusView();

        StartCoroutine(_campusHandler.GetAllCampus(OnCampusFound));

        getButton.onClick.AddListener(ClickFindButton);
    }

    #region -- Add Components --
    private void AddComponetCampusView()
    {
        if (_SpawnCampusView == null)
        {
            _SpawnCampusView = gameObject.AddComponent<SpawnCampusV>();
        }
        else
        {
            Debug.Log("The SpawnCampusV component already exists");
        }
    }

    private void AddComponentCampusHandler()
    {
        if (_campusHandler == null)
        {
            _campusHandler = gameObject.AddComponent<GetCampusH>();
        }
        else
        {
            Debug.Log("The GetCampusH component already exists");
        }
    }
    #endregion

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

    #endregion

    #region -- Fields --

    private IGetCampusHandler _campusHandler;
    private ICampusViewSpawner _SpawnCampusView;

    public Button getButton;

    public TMP_Dropdown findNameInput;

    #endregion
}