using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Utilities;

public class GetStudentHistoryC : MonoBehaviour, IGetContainerHistoryStudentCommand
{
    #region -- Implements --

    public void OnHistoryFound(string dateTime, List<HistoryDStudent> histories)
    {
        if (string.IsNullOrEmpty(dateTime))
        {
            Debug.LogError($"None data history found!");
        }

        _spawnCard.CreateContainer(dateTime, histories);
    }

    #endregion

    #region -- Methods --

    private void Awake()
    {
        AddComponent();
    }

    private void OnEnable()
    {
        if (MainHandler.PrefabList.Count > 0)
            MainHandler.ClearSpawnedPrefabs();

        if (MainHandler.PrefabList.Count <= 1)
            StartCoroutine(_historyHandler.GetAllHistoryByPrinter(MainUser.STUDENT_ID, OnHistoryFound, MainView.OnSuccess, MainView.OnFailed));

        SetUpDefaultData();
    }

    #region -- Add Component --
    private void AddComponent()
    {
        try
        {
            if (_historyHandler == null)
                _historyHandler = this.gameObject.AddComponent<GetStudentHistoryH>();

            if (_spawnCard == null)
                _spawnCard = this.gameObject.AddComponent<SpawnStudentContainerHistoryV>();
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }
    #endregion

    #region -- Set Up Default Data --
    private void SetUpDefaultData()
    {
        List<string> status = EnumProperties.ConvertEnumToList<PrinterDocStatus>("Tất cả");
        _status.ClearOptions();
        _status.AddOptions(status);
    }
    #endregion

    #region -- On Show History --
    public void OnShowHistory(int type)
    {
        MainHandler.ClearSpawnedPrefabs();

        if (MainHandler.PrefabList.Count > 1)
            return;

        int status = _status.value;
        if (type == 0)
        {
            if (status == 1)
            {
                StartCoroutine(_historyHandler.GetHistoryByPrinter(MainUser.STUDENT_ID, "In+Progress", OnHistoryFound, OnSuccess, OnFailed));
                return;
            }
            else if (status == 2)
            {
                StartCoroutine(_historyHandler.GetHistoryByPrinter(MainUser.STUDENT_ID, "Done", OnHistoryFound, OnSuccess, OnFailed));
                return;
            }

            StartCoroutine(_historyHandler.GetAllHistoryByPrinter(MainUser.STUDENT_ID, OnHistoryFound, OnSuccess, OnFailed));
        }
        else
            StartCoroutine(_historyHandler.GetHistoryByPayment(MainUser.STUDENT_ID, OnHistoryFound, OnSuccess, OnFailed));
    }

    private void OnSuccess(string message)
    {
        MainView.OnDebugged(message);
        _noneData.SetActive(false);
    }

    private void OnFailed(string message)
    {
        MainView.OnDebugged(message);
        _noneData.SetActive(true);
    }
    #endregion

    #endregion

    #region -- Fields --

    private IHistoryStudentViewSpawner _spawnCard;
    private IHistoryStudentHandler _historyHandler;

    [SerializeField] private GameObject _noneData;

    [SerializeField] private TMP_Dropdown _status;

    #endregion
}
