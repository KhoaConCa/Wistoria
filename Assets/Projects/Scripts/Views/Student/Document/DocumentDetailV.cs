using UnityEngine;
using TMPro;
using System.Collections.Generic;

#region -- Class Description --
/// <summary>
/// View class for displaying and editing document properties through a UI.
/// Manages dropdown options for paper size, paper type, and page orientation,
/// and allows retrieval of edited document data.
/// </summary>
#endregion
public class DocumentDetailV : MonoBehaviour, IDocumentDataEditor, IDocumentDisplay, IDropdownInitializer
{
    #region -- Unity Methods --

    /// <summary>
    /// Unity's Start method.
    /// Initializes dropdowns with options and displays default document properties.
    /// </summary>
    private void Start()
    {
        // Cast 'this' to IDropdownInitializer to access the explicit method
        var initializer = (IDropdownInitializer)this;
        initializer.InitializeDropdown(paperSizeDropdown, paperSizes);
        initializer.InitializeDropdown(paperSideDropdown, paperTypes);
        initializer.InitializeDropdown(pageOrientationDropdown, pageOrientations);

        _documentData = new DocumentDetailD();
        DisplayDocumentProperties(_documentData);
    }

    #endregion

    #region -- Public Methods --

    /// <summary>
    /// Displays document properties on the UI by setting the dropdown values.
    /// </summary>
    /// <param name="documentData">Document data to display.</param>
    public void DisplayDocumentProperties(DocumentDetailD documentData)
    {
        paperSizeDropdown.value = paperSizes.IndexOf(documentData.PaperSize);
        paperSideDropdown.value = paperTypes.IndexOf(documentData.PaperType);
        pageOrientationDropdown.value = pageOrientations.IndexOf(documentData.PageOrientation);
    }

    /// <summary>
    /// Retrieves the edited document data from the UI.
    /// </summary>
    /// <returns>A DocumentDetailD object with the updated properties.</returns>
    public DocumentDetailD GetEditedDocumentData()
    {
        return new DocumentDetailD
        {
            PaperSize = paperSizes[paperSizeDropdown.value],
            PaperType = paperTypes[paperSideDropdown.value],
            PageOrientation = pageOrientations[pageOrientationDropdown.value],
        };
    }

    #endregion

    #region -- Explicit Interface Implementation --

    /// <summary>
    /// Initializes a dropdown with the provided options.
    /// </summary>
    /// <param name="dropdown">The TMP_Dropdown to initialize.</param>
    /// <param name="options">List of options to add to the dropdown.</param>
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

    #endregion

    #region -- Fields --

    [Header("Dropdown UI Elements")]
    public TMP_Dropdown paperSizeDropdown;
    public TMP_Dropdown paperSideDropdown;
    public TMP_Dropdown pageOrientationDropdown;

    private DocumentDetailD _documentData;

    // Lists of options for each document property
    private List<string> paperSizes = new List<string> { "A4", "A3", "Letter", "Legal" };
    private List<string> paperTypes = new List<string> { "One", "Two" };
    private List<string> pageOrientations = new List<string> { "Portrait", "Landscape" };

    #endregion
}
