using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utilities;

public class LogInC : MonoBehaviour, ILogInScene
{
    #region -- Implements --

    public async void OnLogIn()
    {
        if (!CheckInCondition())
        {
            _textBoxHandler.OnError("Vui lòng nhập mã sinh viên!");
            return;
        }

        string id = GetUserName();

        await GetStudent(id);

        CheckLoadScene();
    }


    #endregion

    #region -- Methods --

    private void Awake()
    {
        AddComponent();
        GetComponent();
    }

    #region -- Add Component --
    private void AddComponent()
    {
        if (_logInH == null)
            _logInH = gameObject.AddComponent<LogInH>();
    }
    #endregion

    #region -- Get Component --
    private void GetComponent()
    {
        if (_textBoxHandler == null && _userName != null)
            _textBoxHandler = _userName.gameObject.GetComponent<UITextBoxV>();
    }
    #endregion

    #region -- Checking Condition --
    private bool CheckInCondition()
    {
        if (_userName.text == "") return false;

        return true;
    }
    #endregion

    #region -- Get Value --
    private string GetUserName()
    {
        return _userName.text;
    }
    #endregion

    #region -- Get Data --
    private async Task GetStudent(string id)
    {
        var taskResult = new TaskCompletionSource<bool>();

        StartCoroutine(_logInH.SearchStudentById(id, onSuccess =>
        {
            MainView.OnDebugged($"Id: {onSuccess}");

            MainUser.STUDENT_ID = onSuccess;

            checkStudent = true;
            taskResult.SetResult(true);
        }, onFaild =>
        {
            MainView.OnDebugged(onFaild);

            GetManager(id).ContinueWith(task => taskResult.SetResult(false));
        }));

        await taskResult.Task;
    }

    private async Task GetManager(string id)
    {
        var taskResult = new TaskCompletionSource<bool>();

        StartCoroutine(_logInH.SearchManagerById(id, onSuccess =>
        {
            MainView.OnDebugged($"Id: {onSuccess}");

            MainUser.MANAGER_ID = onSuccess;

            checkManager = true;
            taskResult.SetResult(true);
        }, onFaild =>
        {
            MainView.OnDebugged(onFaild);
            taskResult.SetResult(false);
        }));

        await taskResult.Task;
    }

    #endregion

    #region -- Check Load Scenee --

    private void CheckLoadScene()
    {
        if (checkStudent)
        {
            LoadGUIScene(_studentScene);
            return;
        }

        if (checkManager)
        {
            LoadGUIScene(_managerScene);
            return;
        }

        _textBoxHandler.OnError("Mã số sinh viên không trùng khớp!");
    }

    #region -- Load Scene
    private void LoadGUIScene(string name)
    {
        SceneManager.LoadScene(name);
    }
    #endregion

    #endregion

    #endregion

    #region -- Fields --

    private ILogInHandler _logInH;
    private ITextBoxHandler _textBoxHandler;

    [SerializeField] private TMP_InputField _userName;

    private readonly string _studentScene = "GUIStudent";
    private readonly string _managerScene = "GUIManager";

    private bool checkStudent = false;
    private bool checkManager = false;

    #endregion
}
