using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Net;
using System;
using Unity.VisualScripting;

public class ManagerCampusC : MonoBehaviour
{
    #region -- Methods --

    void Start()
    {
        if (managerCampusHandler == null)
        {
            managerCampusHandler = gameObject.AddComponent<ManagerCampusH>();
        }

        getButton.onClick.AddListener(ClickGetButton);
    }

    /// <summary>
    /// When clicking button, GET request will be sent to the server
    /// </summary>
    void ClickGetButton()
    {
        string campusName = findNameInput.text.Trim();

        if (!string.IsNullOrEmpty(campusName))
        {
            StartCoroutine(managerCampusHandler.GetCampus(campusName, campus =>
            {
                if (campus != null)
                {
                    Debug.Log($"Found Campus: {campus.CampusName}, Room: {campus.Room}");
                }
                else
                {
                    Debug.Log("Campus not found.");
                }

                // Print the dictionary content for verification
                foreach (var entry in managerCampusHandler.campusDictionary)
                {
                    Debug.Log($"ID: {entry.Key}, Name: {entry.Value.CampusName}");
                }
            }));
        }
        else
        {
            Debug.Log("Campus name cannot be empty.");
        }
    }

    #endregion

    #region -- Fields --

    public TMP_InputField campusNameInput;
    public TMP_InputField campusRoomInput;
    public TMP_InputField findNameInput;

    public Button getButton;
    public Button createButton;

    public ManagerCampusH managerCampusHandler;

    #endregion
}
