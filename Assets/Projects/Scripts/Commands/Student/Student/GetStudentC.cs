using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utilities;

public class GetStudentC : MonoBehaviour, ILogOutScene
{
    #region -- Implements --

    public void OnLogOut()
    {
        MainUser.STUDENT_ID = "";
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
        GetStudentData();
    }

    #region -- Add Component --
    private void AddComponent()
    {
        if (_studentHandler == null)
            _studentHandler = gameObject.AddComponent<GetStudentH>();
    }
    #endregion

    #region -- Get Student Data --
    private void GetStudentData()
    {
        if (!string.IsNullOrEmpty(MainUser.STUDENT_ID))
            StartCoroutine(_studentHandler.GetStudentByID(MainUser.STUDENT_ID, SetUpData, onFailed =>
            {
                MainView.OnReset(GetStudentData, onFailed);
            }));
    }
    #endregion

    #region -- Set Up Data --
    private void SetUpData(StudentD data)
    {
        Initialization(_paper, _tagName, data.Paper.ToString());
        Initialization(_studentName, _tagName, data.FullName);
        Initialization(_dayOfBirth, _tagName, data.DateOfBirth.ToString("dd/MM/yyyy"));
        Initialization(_phoneNumber, _tagName, data.PhoneNumber);
        Initialization(_email, _tagName, data.Email);
        Initialization(_studentId, _tagName, data.StudentID);
        Initialization(_class, _tagName, data.Class);
        Initialization(_course, _tagName, $"K{data.Course}");
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

    #region -- On Click Event --
    public void OnClickStore()
    {
        GameObject buttonObject = GameObject.FindWithTag(_tagButton);
        Button button = buttonObject.GetComponent<Button>();
        button.onClick.Invoke();
    }
    #endregion

    #endregion

    #region -- Fields --

    private IGetStudentHandler _studentHandler;

    private UISpawnFeatureV _uiTransform;

    [SerializeField] private GameObject _paper;
    [SerializeField] private GameObject _studentName;
    [SerializeField] private GameObject _dayOfBirth;
    [SerializeField] private GameObject _phoneNumber;
    [SerializeField] private GameObject _email;
    [SerializeField] private GameObject _studentId;
    [SerializeField] private GameObject _class;
    [SerializeField] private GameObject _course;

    private readonly string _tagName = "MessageValue";
    private readonly string _tagButton = "StoreButton";

    #endregion
}
