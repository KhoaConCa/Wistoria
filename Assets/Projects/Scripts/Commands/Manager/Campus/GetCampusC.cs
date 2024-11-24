using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using Utilities;

public class GetCampusC : MonoBehaviour, IGetCampusCommand
{
    #region -- Implements --

    /// <summary>
    /// Handles the response from the server when campus information is found.
    /// Logs the details of the campus if it exists
    /// </summary>
    /// <param name="campus">The campus object returned from the server</param>
    public void OnCampusFound(CampusD campus)
    {
        if (campus != null)
            _spawnCampusView.CreateCard(campus);
        else
            Debug.Log("Campus not found.");
    }

    #endregion

    #region -- Methods --

    private void Awake()
    {
        AddComponentCampusHandler();
        AddComponetCampusView();
    }

    private void OnEnable()
    {
        try
        {
            MainHandler.ClearSpawnedPrefabs();

            if (MainHandler.PrefabList.Count <= 0)
            {
                StartCoroutine(_campusHandler.GetAllCampus(OnCampusFound));
            }

            StartCoroutine(_campusHandler.GetUniqueName(GetCampusUniqueName));
        }
        catch (Exception e)
        {
            Debug.LogWarning(e.Message);
        }
    }

    #region -- Add Components --
    private void AddComponetCampusView()
    {
        if (_spawnCampusView == null)
        {
            _spawnCampusView = gameObject.AddComponent<SpawnCampusV>();
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

    public void GetCampusUniqueName(List<string> campusName)
    {
        _uniqueName = campusName;
        campusName.Insert(0, "Tất cả");

        _nameCampusComboBox.ClearOptions();

        _nameCampusComboBox.AddOptions(campusName);
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
        int selectedIndex = _nameCampusComboBox.value;
        return _nameCampusComboBox.options[selectedIndex].text;
    }

    public void SearchCampusByName()
    {
        try
        {
            string name = _nameCampusComboBox.captionText.text;
            if (name == _uniqueName[0])
                StartCoroutine(_campusHandler.GetAllCampus(OnCampusFound));
            else
                StartCoroutine(_campusHandler.GetCampus(name, OnCampusFound));
        }
        catch (Exception e) 
        {
            Debug.LogError(e.Message); 
        }
    }

    #endregion

    #region -- Fields --

    private IGetCampusHandler _campusHandler;
    private ICampusViewSpawner _spawnCampusView;

    private List<string> _uniqueName;

    [SerializeField] private Button _getButton;

    [SerializeField] private TMP_Dropdown _nameCampusComboBox;

    #endregion
}