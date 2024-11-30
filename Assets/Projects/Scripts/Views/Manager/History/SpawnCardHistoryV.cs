using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Utilities;

public class SpawnCardHistoryV : MonoBehaviour, ICardHistoryViewSpawner
{
    #region -- Implements --

    public void GetData(List<HistoryD> histories)
    {
        if (histories.Count <= 0)
       {
            Debug.LogError("None data histories!");
            return;
       }

       _histories = histories;
       GenerateCard();
       _histories.Clear();
    }

    public void CreateCard(HistoryD history)
    {
        if (this.gameObject == null)
        {
            Debug.LogError("History container can't be found!");
            return;
        }

        MainHandler.SpawnPrefabByLabel(_historyPrefab, this.gameObject, (spawnedPrefab) =>
        {
            if (spawnedPrefab != null)
            {
                _cardData = spawnedPrefab.GetComponent<HistoryCardData>();
                _cardData.Initialize(history);

                spawnedPrefab.transform.SetParent(this.gameObject.transform, false);

                SetCardData(_tagName, _cardData.Name);
                SetCardData(_tagStudent, _cardData.StudentName);
                SetCardData(_tagTime, _cardData.DateProcess.ToString("HH:mm - dd/MM/yyyy"));

                if (_cardData.TypeData == 0)
                    SetCardData(_tagPaper, "+" + _cardData.Paper.ToString());
                else
                    SetCardData(_tagPaper, "-" + _cardData.Paper.ToString());
            }
            else
            {
                Debug.LogError("Failed to spawn prefab!");
            }
        });
    }

    #endregion

    #region -- Methods --

    private void Awake()
    {
        GetComponentDefault();
    }

    #region -- Get Component --
    private void GetComponentDefault()
    {
        try
        {
            if (_setDataHistoryView != null || _historyPrefab.labelString != "")
                return;

            _historyPrefab = new AssetLabelReference { labelString = _labelPrefab };
            _setDataHistoryView = this.gameObject.GetComponentInParent<SetDataHistoryV>();
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }
    #endregion

    #region -- Generate Card --
    private void GenerateCard()
    {
        foreach (HistoryD history in _histories)
        {
            CreateCard(history);
        }
    }
    #endregion

    #region -- Set Up Data Prefab --

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
    private IHistoryCardData _cardData;

    private List<HistoryD> _histories = new List<HistoryD>();

    [SerializeField] private AssetLabelReference _historyPrefab;

    private readonly string _labelPrefab = "HistoryManager";

    private readonly string _tagName = "ValueName";
    private readonly string _tagStudent = "ValueStudent";
    private readonly string _tagTime = "ValueTime";
    private readonly string _tagPaper = "ValuePaper";

    #endregion
}