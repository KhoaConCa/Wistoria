using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using Utilities;

public class GetHistoryC : MonoBehaviour, IGetContainerHistoryCommand
{
    #region -- Implements --

    public void OnHistoryFound(string dateTime, List<HistoryD> histories)
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
            StartCoroutine(_historyHandler.GetAllHistory(OnHistoryFound, MainView.OnSuccess, MainView.OnFaild));
    }

    #region -- Add Component --
    private void AddComponent()
    {
        try
        {
            if (_historyHandler == null)
                _historyHandler = this.gameObject.AddComponent<GetHistoryH>();

            if (_spawnCard == null)
                _spawnCard = this.gameObject.AddComponent<SpawnContainerHistoryV>();
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
            StartCoroutine(_historyHandler.GetAllHistory(OnHistoryFound, MainView.OnSuccess, MainView.OnFaild));
        else if (type == 1)
            StartCoroutine(_historyHandler.GetHistoryByPrinter(OnHistoryFound, MainView.OnSuccess, MainView.OnFaild));
        else
            StartCoroutine(_historyHandler.GetHistoryByPayment(OnHistoryFound, MainView.OnSuccess, MainView.OnFaild));
    }
    #endregion

    #endregion

    #region -- Fields --

    private IHistoryViewSpawner _spawnCard;
    private IHistoryHandler _historyHandler;

    #endregion
}
