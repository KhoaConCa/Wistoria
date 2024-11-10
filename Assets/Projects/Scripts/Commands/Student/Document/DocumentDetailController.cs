using UnityEngine;
using UnityEngine.UI;

#region -- Class Description --
/// <summary>
/// Controller class for managing document details.
/// It initializes the document with default data, displays it on the view, and handles updates to document properties.
/// </summary>
#endregion
public class DocumentDetailController : MonoBehaviour
{

    #region -- Unity Methods --

    /// <summary>
    /// Unity's Start method.
    /// Initializes the view and document data with default values and displays them.
    /// </summary>
    private void Start()
    {
        _view = GetComponent<DocumentDetailV>();

        // Initialize document data with default values
        _documentData = new DocumentDetailD();

        // Display default document properties on the view
        _view.DisplayDocumentProperties(_documentData);
    }

    #endregion

    #region -- Fields --

    private DocumentDetailV _view;
    private DocumentDetailD _documentData;

    #endregion
}
