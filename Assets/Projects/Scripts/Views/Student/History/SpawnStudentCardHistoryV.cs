using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
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
                    PaymentPrefab(spawnedPrefab);
                else
                {
                    _cardData.PrinterDoc.Printer = ProcessingJson.InitializaProperty<StudentPrinterD>(_cardData.PrinterDoc.PrinterRaw);
                    _cardData.PrinterDoc.Document = ProcessingJson.InitializaProperty<DocumentDStudent>(_cardData.PrinterDoc.DocumentRaw);
                    SetUpPrinterDocPrefab(spawnedPrefab);
                }
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
        foreach (HistoryDStudent history in _histories)
        {
            CreateCard(history);
        }
    }
    #endregion

    #region -- Set Up Payment Prefab --

    private void PaymentPrefab(GameObject prefab)
    {
        _handler = gameObject.GetComponentInParent<GetStudentHistoryH>();
        StartCoroutine(_handler.GetPackageById(_cardData.Payment.PaperID, onSuccess =>
        {
            _cardData.Payment.UpdatePaper(onSuccess);

            SetCardData(_tagName, $"Thanh toán gói {_cardData.Payment.PaperData.Paper} giấy");

            SetCardData(_labelCampus, "Phương thức:");
            SetCardData(_tagCampus, "MoMo");

            SetCardData(_tagTime, _cardData.DateProcess?.ToString("HH:mm - dd/MM/yyyy"));

            SetCardData(_labelPaper, "Đơn giá:");
            CultureInfo vietnamCulture = new CultureInfo("vi-VN");
            SetCardData(_tagPaper, "- " + _cardData.Payment.PaperData.Price.ToString("N0", vietnamCulture) + "đ");

            if (_cardData.Payment.Status == PaymentStatus.Success.ToString())
                SetUpIcon(prefab.transform, "SuccessMarker");
            else
                SetUpIcon(prefab.transform, "FailedMarker");
        }));
    }

    #endregion

    #region -- Set Up PrinterDoc Prefab --
    private void SetUpPrinterDocPrefab(GameObject prefab)
    {
        SetCardData(_tagName, _cardData.PrinterDoc.Document.Name);
        SetCardData(_tagCampus, _cardData.PrinterDoc.Printer.LocateAt.Name);
        SetCardData(_tagPaper, CalculatePaper(_cardData).ToString() + " trang");

        if (_cardData.DateProcess != null)
        {
            SetCardData(_tagTime, _cardData.DateProcess?.ToString("HH:mm - dd/MM/yyyy"));
            SetUpIcon(prefab.transform, "SuccessMarker");
        }
        else
        {
            SetCardData(_tagTime, "Chưa hoàn thành in ấn!");
            SetUpIcon(prefab.transform, "WarningMarker");
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

    #region -- Set Up Icon --
    private async void SetUpIcon(Transform prefabTrans, string tagIcon)
    {
        Transform imageTrans = MainView.FindObjectsByTag(prefabTrans, "Icon");
        Image image = imageTrans.GetComponent<Image>();

        AssetLabelReference icon = new AssetLabelReference { labelString = tagIcon};
        var handle = Addressables.LoadAssetAsync<Sprite>(icon);
        Sprite sprite = await handle.Task;

        if (sprite == null)
        {
            Debug.LogError($"Sprite not found at address: {icon}");
        }
        else
        {
            image.sprite = sprite;
        }

        // Dọn dẹp tài nguyên sau khi dùng
        Addressables.Release(handle);
    }
    #endregion

    #region -- Calculate Paper --
    private int CalculatePaper(IHistoryCardDStudent cardData)
    {
        return ((cardData.PrinterDoc.PageEnd - cardData.PrinterDoc.PageBegin + 1)
            / cardData.PrinterDoc.Side) * cardData.PrinterDoc.Copies;
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