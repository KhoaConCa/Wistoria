using UnityEngine;
using UnityEngine.UI;

public class DocumentDetailController : MonoBehaviour, IDocumentInitialization, IDocumentUpdater
{
    #region -- Implements --

    public void InitializeDocument()
    {
        _view = GetComponent<IDocumentDataEditor>();
        
        _view.DisplayDocumentProperties(_documentData);
    }

    public void UpdateDocumentData(PrinterDocCard newData)
    {
        _documentData = newData;
        _view.DisplayDocumentProperties(_documentData);
    }

    #endregion

    #region -- Methods --

    private void Start()
    {
        InitializeDocument();

        _retriever = GetComponent<DocumentDataRetrieverH>();
        if (_retriever == null)
        {
            Debug.LogError("IDocumentRetriever is not attached to this GameObject!");
        }

        // Setup button click listener
        if (retrieveDataButton != null)
        {
            retrieveDataButton.onClick.AddListener(OnRetrieveButtonClicked);
        }
        else
        {
            Debug.LogError("Retrieve Data Button is not assigned in the Inspector.");
        }
    }

    private void OnRetrieveButtonClicked()
    {
        if (_retriever != null)
        {
            _retriever.RetrieveDocumentData();
        }
        else
        {
            Debug.LogError("IDocumentRetriever is not initialized.");
        }
    }

    #endregion

    #region -- Fields --

    [Header("Retrieve Data Button")]
    public Button retrieveDataButton;

    private IDocumentDataEditor _view;
    private IDocumentRetriever _retriever;

    private PrinterDocCard _documentData;

    #endregion
}
