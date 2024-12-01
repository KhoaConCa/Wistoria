using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utilities;

public class GetManagerC : MonoBehaviour, ILogOutScene
{
    #region -- Implements --

    public void OnLogOut()
    {
        MainUser.MANAGER_ID = "";
        SceneManager.LoadScene("GUILogIn");
    }

    #endregion

    #region -- Methods --

    private void Awake()
    {
        AddComponent();
    }

    private void OnEnable()
    {
        GetManagerData();
    }

    #region -- Add Component --
    private void AddComponent()
    {
        if (_managerHandler == null)
            _managerHandler = gameObject.AddComponent<GetManagerH>();
    }
    #endregion

    #region -- Get Manager Data --
    private void GetManagerData()
    {
        if (!string.IsNullOrEmpty(MainUser.MANAGER_ID))
            StartCoroutine(_managerHandler.GetManagerByID(MainUser.MANAGER_ID, SetUpData, onFailed =>
            {
                MainView.OnReset(GetManagerData, onFailed);
            }));
    }
    #endregion

    #region -- Set Up Data --
    private void SetUpData(ManagerD data)
    {
        Initialization(_managerName, _tagName, data.FullName);
        Initialization(_dayOfBirth, _tagName, data.DateOfBirth.ToString("dd/MM/yyyy"));
        Initialization(_phoneNumber, _tagName, data.PhoneNumber);
        Initialization(_email, _tagName, data.Email);
        Initialization(_managerId, _tagName, data.ManagerID);
    }

    private void Initialization(GameObject objectValue, string tagName, string textValue)
    {
        Transform objectText = MainView.FindObjectsByTag(objectValue.transform, tagName);

        if (objectText == null)
        {
            MainView.OnDebugged("Failed to find text component!");
            return;
        }

        TextMeshProUGUI value = objectText.GetComponent<TextMeshProUGUI>();
        value.text = textValue;
    }
    #endregion

    #endregion

    #region -- Fields --

    private IGetManagerH _managerHandler;

    [SerializeField] private GameObject _managerName;
    [SerializeField] private GameObject _dayOfBirth;
    [SerializeField] private GameObject _phoneNumber;
    [SerializeField] private GameObject _email;
    [SerializeField] private GameObject _managerId;

    private readonly string _tagName = "MessageValue";

    #endregion
}
