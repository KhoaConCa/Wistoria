using UnityEngine;

public class DocumentDetailController : MonoBehaviour, IDocumentInitialization, IDocumentUpdater
{
    private IDocumentDisplay _view;
    private DocumentDetailD _documentData;

    private void Start()
    {
        InitializeDocument();
    }

    public void InitializeDocument()
    {
        _view = GetComponent<DocumentDetailV>();
        _documentData = new DocumentDetailD
        {
            UseDefaultPages = true,
            CustomPages = "",
            NoCopies = true,
            CustomCopies = 0
        };

        _view.DisplayDocumentProperties(_documentData);
    }

    public void UpdateDocumentData(DocumentDetailD newData)
    {
        _documentData = newData;
        _view.DisplayDocumentProperties(_documentData);
    }
}
