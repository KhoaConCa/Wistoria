using UnityEngine;
using UnityEngine.UI;

#region -- Class Description --
/// <summary>
/// Controller class for managing document details.
/// It initializes the document with default data, displays it on the view, and handles updates to document properties.
/// </summary>
#endregion
public class DocumentDetailController : MonoBehaviour, IDocumentInitialization, IDocumentUpdater
{
    #region -- Unity Methods --

    /// <summary>
    /// Unity's Start method.
    /// Initializes the view and document data with default values and displays them.
    /// </summary>
    private void Start()
    {
        InitializeDocument();
    }

    #endregion

    #region -- Implementations of IDocumentInitialization --

    /// <summary>
    /// Initializes document data with default values and displays it on the view.
    /// </summary>
    public void InitializeDocument()
    {
        _view = GetComponent<DocumentDetailV>();
        _documentData = new DocumentDetailD();

        // Display default document properties on the view
        _view.DisplayDocumentProperties(_documentData);
    }

    #endregion

    #region -- Implementations of IDocumentUpdater --

    /// <summary>
    /// Updates document properties based on new data and reflects changes on the view.
    /// </summary>
    /// <param name="newData">The new data for updating the document properties.</param>
    public void UpdateDocumentData(DocumentDetailD newData)
    {
        _documentData = newData;

        // Update the view with the new document properties
        _view.DisplayDocumentProperties(_documentData);
    }

    #endregion

    #region -- Fields --

    private IDocumentDisplay _view; // Interface for displaying data on the view
    private DocumentDetailD _documentData; // Changed to concrete type

    #endregion
}
