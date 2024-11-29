using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
using Utilities;

public class SpawnContainerHistoryV : MonoBehaviour, IHistoryViewSpawner
{
    #region -- Implements --

    /// <summary>
    /// Using addressable to create prefab
    /// </summary>
    /// <param name="history">Data of history</param>
    public void CreateContainer(string dateTime, List<HistoryD> histories)
    {
        Debug.Log(histories.Count);
        GetComponentDefault();
        GenerateContainerPrefab(dateTime, histories);
    }

    #endregion

    #region -- Methods --

    void Awake()
    {
        AddComponentDefault();
        GetComponentDefault();
    }

    #region -- Add Component --
    private void AddComponentDefault()
    {
        try
        {
            if (_setDataHistoryView == null)
                _setDataHistoryView = gameObject.AddComponent<SetDataHistoryV>();
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }
    #endregion

    #region -- Get Component --
    private void GetComponentDefault()
    {
        try
        {
            if (_historyContainerPrefab == null)
                _historyContainerPrefab = new AssetLabelReference { labelString = _labelContainerPrefab };

            if (_objectContain == null)
                _objectContain = GameObject.FindWithTag(_tagObjectContainer);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }
    #endregion

    #region - Generate Prefab -
    private void GenerateContainerPrefab(string objectDateTime, List<HistoryD> histories)
    {
        _objectContain = GameObject.FindGameObjectWithTag(_tagObjectContainer);
        MainHandler.SpawnPrefabByLabel(_historyContainerPrefab, _objectContain, spawnedPrefab =>
        {
            if (spawnedPrefab == null)
            {
                Debug.Log("Can't spawn container prefab");
                return;
            }
            spawnedPrefab.name = $"HistoryContainer #{_count++}";
            spawnedPrefab.transform.SetParent(_objectContain.transform, false);

            _containerData = spawnedPrefab.GetComponent<HistoryContainerData>();
            _containerData.Initialize(objectDateTime, histories);

            SetCardData(_tagMonth, $"Tháng {_containerData.Date}");

            ICardHistoryViewSpawner detailCard = spawnedPrefab.GetComponent<SpawnCardHistoryV>();
            detailCard.GetData(_containerData.Histories);
        });
    }
    #endregion

    #region -- Set Up Data Prefab --
    private IHistoryCardData GetComponentCard(GameObject cardPrefab)
    {
        return cardPrefab.GetComponent<IHistoryCardData>();
    }

    private void SetCardData(string tagName, string value)
    {
        Transform valueLocation = FindValueData(ref tagName);
        _setDataHistoryView.AddComponentFromPrefab(valueLocation);

        _setDataHistoryView.SetHistoryData(value);
    }

    private Transform FindValueData(ref string tagName)
    {
        return MainHandler.FindChildObjectsByTag(MainHandler.LastSpawnedPrefab.transform, tagName);
    }

    #endregion

    #endregion

    #region -- Fields --

    private IHistoryDataSetter _setDataHistoryView;
    private IHistoryContainerData _containerData;

    [SerializeField] private GameObject _objectContain;

    [SerializeField] private AssetLabelReference _historyContainerPrefab;

    private readonly string _labelContainerPrefab = "HistoryContainerManager";

    private readonly string _tagObjectContainer = "ObjectContain";
    private readonly string _tagMonth = "ValueMonth";

    private int _count = 0;

    #endregion
}
