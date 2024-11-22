using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class DocumentDetailV : MonoBehaviour, IDocumentDataEditor, IDocumentDisplay, IDropdownInitializer
{
    [Header("Dropdown UI Elements")]
    public TMP_Dropdown paperSizeDropdown;
    public TMP_Dropdown paperSideDropdown;
    public TMP_Dropdown pageOrientationDropdown;

    [Header("Toggle and Input UI Elements")]
    public Toggle toggleDefaultPages;
    public Toggle toggleCustomPages;
    public TMP_InputField inputCustomPages;

    public Toggle toggleNoCopies;
    public Toggle toggleCustomCopies;
    public TMP_InputField inputCustomCopies;

    private DocumentDetailD _documentData;

    private List<string> paperSizes = new List<string> { "A4", "A3"};
    private List<string> paperTypes = new List<string> { "Một mặt", "Hai mặt" };
    private List<string> pageOrientations = new List<string> { "Portrait", "Landscape" };

    private void Start()
    {
        InitializeDropdowns();
        InitializeToggles();
        _documentData = new DocumentDetailD();
        DisplayDocumentProperties(_documentData);
    }

    private void InitializeDropdowns()
    {
        var initializer = (IDropdownInitializer)this;
        initializer.InitializeDropdown(paperSizeDropdown, paperSizes);
        initializer.InitializeDropdown(paperSideDropdown, paperTypes);
        initializer.InitializeDropdown(pageOrientationDropdown, pageOrientations);
    }

    private void InitializeToggles()
    {
        // Initialize toggles and their behavior
        toggleDefaultPages.isOn = true;
        toggleCustomPages.isOn = false;
        inputCustomPages.interactable = false;

        toggleNoCopies.isOn = true;
        toggleCustomCopies.isOn = false;
        inputCustomCopies.interactable = false;

        // Add listeners to toggle changes
        toggleDefaultPages.onValueChanged.AddListener(isOn =>
        {
            if (isOn) inputCustomPages.interactable = true;
        });

        toggleCustomPages.onValueChanged.AddListener(isOn =>
        {
            if (isOn) inputCustomPages.interactable = false;
        });

        toggleNoCopies.onValueChanged.AddListener(isOn =>
        {
            if (isOn) inputCustomCopies.interactable = true;
        });

        toggleCustomCopies.onValueChanged.AddListener(isOn =>
        {
            if (isOn) inputCustomCopies.interactable = false;
        });
    }

    public void DisplayDocumentProperties(DocumentDetailD documentData)
    {
        paperSizeDropdown.value = paperSizes.IndexOf(documentData.PaperSize);
        paperSideDropdown.value = paperTypes.IndexOf(documentData.PaperType);
        pageOrientationDropdown.value = pageOrientations.IndexOf(documentData.PageOrientation);

        toggleDefaultPages.isOn = documentData.UseDefaultPages;
        toggleCustomPages.isOn = !documentData.UseDefaultPages;
        inputCustomPages.text = documentData.CustomPages;

        toggleNoCopies.isOn = documentData.NoCopies;
        toggleCustomCopies.isOn = !documentData.NoCopies;
        inputCustomCopies.text = documentData.CustomCopies.ToString();
    }

    public DocumentDetailD GetEditedDocumentData()
    {
        return new DocumentDetailD
        {
            PaperSize = paperSizes[paperSizeDropdown.value],
            PaperType = paperTypes[paperSideDropdown.value],
            PageOrientation = pageOrientations[pageOrientationDropdown.value],

            UseDefaultPages = toggleDefaultPages.isOn,
            CustomPages = toggleCustomPages.isOn ? inputCustomPages.text : "",

            NoCopies = toggleNoCopies.isOn,
            CustomCopies = toggleCustomCopies.isOn ? int.Parse(inputCustomCopies.text) : 0
        };
    }

    void IDropdownInitializer.InitializeDropdown(TMP_Dropdown dropdown, List<string> options)
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
}
