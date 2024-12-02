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
            Debug.LogError("Checking user name failed!");
            return;
        }

        string id = GetUserName();
        await GetStudent(id); // Chờ GetStudent hoàn thành
        CheckLoadScene();
    }


    #endregion

    #region -- Methods --

    private void Awake()
    {
        AddComponent();
    }

    #region -- Add Component --
    private void AddComponent()
    {
        if (_logInH == null)
            _logInH = gameObject.AddComponent<LogInH>();
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

        MainView.OnDebugged("Failed to log in!");
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

    [SerializeField] private TMP_InputField _userName;

    private readonly string _studentScene = "GUIStudent";
    private readonly string _managerScene = "GUIManager";

    private bool checkStudent = false;
    private bool checkManager = false;

    #endregion
}
