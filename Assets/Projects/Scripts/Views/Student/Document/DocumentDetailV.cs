using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using UnityEditor;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

public class DocumentDetailV : MonoBehaviour, IDocumentDefailV
{
    #region -- Implements --

    public void DefaultDocument()
    {
        GameObject mainUI = GameObject.FindWithTag("MainUI");
        _transform = mainUI.GetComponent<UITransformV>();
        _transform.SetActiveObjectUI(gameObject);

        SetUpDropdowns();
        InitializeToggles();
        InitializeInputField();
    }

    public void GetDocument()
    {
        if (CheckComboBox() && CheckTextBox())
        {
            _cardData.PaperSize = GetValueDropDown<PaperSize>(_size);

            var value = GetValueDropDown<PaperSide>(_side);
            _cardData.Side = EnumProperties.GetEnumIdByName<PaperSide>(value.ToString()) + 1;

            _cardData.Orientation = GetValueDropDown<Orientation>(_orientation);

            GetPageValue();
            GetCopyValue();
            GetColorValue();

            _transform.SetActiveObjectUI(_targetObject);
        }
    }

    #endregion

    #region -- Methods --

    private void Awake()
    {
        GameObject mainUI = GameObject.FindWithTag("MainUI");
        _transform = mainUI.GetComponent<UITransformV>();
    }

    private void OnEnable()
    {
        _nameFile.SetActive(true);
        _nameFile.GetComponent<TextMeshProUGUI>().text = _cardData.Document.Name;

        GameObject container = GameObject.FindWithTag("ObjectContain");
        RectTransform transform = container.GetComponent<RectTransform>();
        transform.localPosition = new Vector2(transform.localPosition.x, 0);
    }

    private void OnDisable()
    {
        _nameFile.SetActive(false);
    }

    #region -- Initialize Component --
    public void InitializeDropdown(TMP_Dropdown dropdown, List<string> options)
    {
        if (dropdown != null)
        {
            dropdown.ClearOptions();
            dropdown.AddOptions(options);
        }
        else
        {
            Debug.LogError("Dropdown is not assigned in the Inspector.");
        }
    }

    private void InitializeToggles()
    {
        _defaultPage.isOn = true;
        _defaultCopy.isOn = true;
        _noneColor.isOn = true;
    }

    private void InitializeInputField()
    {
        _pageInput.text = "";
        _pageInput.interactable = false;

        _copyInput.text = "";
        _copyInput.interactable = false;
    }
    #endregion

    #region -- Set Up Data --
    private void SetUpDropdowns()
    {
        InitializeDropdown(_size, EnumProperties.ConvertEnumToList<PaperSize>("- Chọn loại giấy -"));
        InitializeDropdown(_side, EnumProperties.ConvertEnumToList<PaperSide>("- Chọn mặt giấy -"));
        InitializeDropdown(_orientation, EnumProperties.ConvertEnumToList<Orientation>("- Chọn chiều giấy -"));
    }
    #endregion

    #region -- Checking Condition --
    private bool CheckComboBox()
    {
        bool isChecking = true;

        if (_size.value == 0)
        {
            Debug.LogError("Khổ giấy chưa được chọn!");
            isChecking = false;
        }

        if (_side.value == 0)
        {
            Debug.LogError("Mặt giấy chưa được chọn!");
            isChecking = false;
        }

        if (_orientation.value == 0)
        {
            Debug.LogError("Chiều giấy chưa được chọn!");
            isChecking = false;
        }

        return isChecking;
    }

    private bool CheckTextBox()
    {
        bool isChecking = true;

        ITextBoxHandler textBoxHandler = null;
        if (_pageInput.IsInteractable())
        {
            string page = _pageInput.text.Replace("-", "").Replace(" ", "");

            if (ContainsSpecialCharacters(page) || ContainsLetters(page) || string.IsNullOrEmpty(page))
            {
                isChecking = false;

                textBoxHandler = _pageInput.GetComponent<UITextBoxV>();
                textBoxHandler.OnError("Định dạng chuẩn: 1-8");
            }
        }

        if (_copyInput.IsInteractable())
        {
            string copy = _copyInput.text;

            if (ContainsSpecialCharacters(copy) || ContainsLetters(copy) || string.IsNullOrEmpty(copy))
            {
                isChecking = false;

                textBoxHandler = _pageInput.GetComponent<UITextBoxV>();
                textBoxHandler.OnError("Định dạng chuẩn chỉ chứa số!");
            }
        }

        return isChecking;
    }

    public bool ContainsSpecialCharacters(string input)
    {
        Regex regex = new Regex(@"[^a-zA-Z0-9]");
        return regex.IsMatch(input);
    }

    public bool ContainsLetters(string input)
    {
        Regex regex = new Regex(@"[a-zA-Z]");
        return regex.IsMatch(input);
    }

    #endregion

    #region -- Get Value --
    private string GetValueDropDown<T>(TMP_Dropdown dropDown) where T : struct, Enum
    {
        if (dropDown == null)
        {
            Debug.LogError("Dropdown không được gán.");
            return null;
        }

        if (dropDown.value == 0)
        {
            Debug.LogError("Vui lòng chọn giá trị khác!");
            return null;
        }

        string selected = dropDown.captionText.text;
        var enumValue = EnumProperties.GetEnumByDescription<T>(selected);

        if (enumValue.HasValue)
        {
            return enumValue.Value.ToString();
        }
        else
        {
            Debug.LogError($"Không tìm thấy giá trị Enum tương ứng với mô tả: {selected}");
        }

        return null;
    }

    private void GetPageValue()
    {
        if (!_defaultPage.isOn)
        {
            string value = _pageInput.text;

            string[] values = value.Split("-");

            _cardData.PageBegin = int.Parse(values[0]);
            _cardData.PageEnd = int.Parse(values[1]);

            return;
        }

        _cardData.PageBegin = 1;
        _cardData.PageEnd = _cardData.Document.Size;
    }

    private void GetCopyValue()
    {
        if (!_defaultCopy.isOn)
        {
            string value = _copyInput.text;
            _cardData.Copies = Convert.ToInt32(value);

            return;
        }

        _cardData.Copies = 1;
    }

    private void GetColorValue()
    {
        if (_noneColor.isOn)
        {
            _cardData.Color = "Black and white";
            return;
        }

        _cardData.Color = "Color";
    }
    #endregion

    #endregion

    #region -- Fields --

    private ITransformUI _transform;

    [Header("Dropdown UI Elements")]
    [SerializeField] private TMP_Dropdown _size;
    [SerializeField] private TMP_Dropdown _side;
    [SerializeField] private TMP_Dropdown _orientation;

    [Header("Toggle UI Elements")]
    [SerializeField] private Toggle _defaultPage;
    [SerializeField] private Toggle _defaultCopy;
    [SerializeField] private Toggle _noneColor;


    [Header("Input field UI Elements")]
    [SerializeField] private TMP_InputField _pageInput;
    [SerializeField] private TMP_InputField _copyInput;

    [Header("Fields Element")]
    [SerializeField] private GameObject _targetObject;
    [SerializeField] private GameObject _nameFile;
    [SerializeField] private PrinterDocCard _cardData;

    #endregion
}
