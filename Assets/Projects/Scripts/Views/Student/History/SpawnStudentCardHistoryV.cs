using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Utilities;

public class SpawnStudentCardHistoryV : MonoBehaviour, ICardHistoryStudentViewSpawner
{
    #region -- Implements --

    public void GetData(List<HistoryDStudent> histories)
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

    public void CreateCard(HistoryDStudent history)
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
                _cardData = spawnedPrefab.GetComponent<HistoryCardDStudent>();
                _cardData.Initialize(history);

                if (_cardData.Payment != null)
                {
                    _handler = gameObject.GetComponentInParent<GetStudentHistoryH>();
                    StartCoroutine(_handler.GetPackageById(_cardData.Payment.PaperID, onSuccess =>
                    {
                        _cardData.Payment.UpdatePaper(onSuccess);

                        SetCardData(_tagName, $"Thanh toán gói {_cardData.Payment.PaperData.Paper} giấy");

                        SetCardData(_labelCampus, "Phương thức:");
                        SetCardData(_tagCampus, "MoMo");

                        SetCardData(_labelPaper, "Đơn giá:");

                        CultureInfo vietnamCulture = new CultureInfo("vi-VN");
                        SetCardData(_tagPaper, "- " + _cardData.Payment.PaperData.Price.ToString("N0", vietnamCulture) + "đ");
                    }));
                }
                else
                {
                    SetCardData(_tagName, _cardData.PrinterDoc.Document.Name);
                    SetCardData(_tagCampus, _cardData.PrinterDoc.Printer.LocateAt.Name);
                    SetCardData(_tagPaper, CalculatePaper(_cardData).ToString() + " trang");
                }

                if (_cardData.DateProcess != null)
                    SetCardData(_tagTime, _cardData.DateProcess?.ToString("HH:mm - dd/MM/yyyy"));
                else
                    SetCardData(_tagTime, "Chưa hoàn thành in ấn!");     
            }
            else
            {
                Debug.LogError("Failed to spawn prefab!");
            }
        });
    }

    private int CalculatePaper(IHistoryCardDStudent cardData)
    {
        return ((cardData.PrinterDoc.PageEnd - cardData.PrinterDoc.PageBegin + 1) 
            / cardData.PrinterDoc.Side) * cardData.PrinterDoc.Copies;
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
        foreach (HistoryDStudent history in _histories)
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

    private IHistoryStudentHandler _handler;
    private IHistoryDataSetter _setDataHistoryView;
    private IHistoryCardDStudent _cardData;

    private List<HistoryDStudent> _histories = new List<HistoryDStudent>();

    [SerializeField] private AssetLabelReference _historyPrefab;

    private readonly string _labelPrefab = "HistoryStudent";

    private readonly string _labelCampus = "LabelCampus";
    private readonly string _labelPaper = "LabelPaper";

    private readonly string _tagName = "ValueName";
    private readonly string _tagCampus = "ValueCampus";
    private readonly string _tagTime = "ValueTime";
    private readonly string _tagPaper = "ValuePaper";

    #endregion
}