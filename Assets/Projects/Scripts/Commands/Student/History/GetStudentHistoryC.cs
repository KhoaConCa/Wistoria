using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor;
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
            return;
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
            StartCoroutine(_historyHandler.GetHistoryByPrinter(MainUser.STUDENT_ID, "In Progress", OnHistoryFound, MainView.OnSuccess, MainView.OnFailed));
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

    #region -- On Show History --
    public void OnShowHistory(int type)
    {
        MainHandler.ClearSpawnedPrefabs();

        if (MainHandler.PrefabList.Count > 1)
            return;

        if (type == 0)
            StartCoroutine(_historyHandler.GetHistoryByPrinter(MainUser.STUDENT_ID, "In Progress", OnHistoryFound, MainView.OnSuccess, MainView.OnFailed));
        else
            StartCoroutine(_historyHandler.GetHistoryByPayment(MainUser.STUDENT_ID, OnHistoryFound, MainView.OnSuccess, MainView.OnFailed));
    }
    #endregion

    #endregion

    #region -- Fields --

    private IHistoryStudentViewSpawner _spawnCard;
    private IHistoryStudentHandler _historyHandler;

    #endregion
}
