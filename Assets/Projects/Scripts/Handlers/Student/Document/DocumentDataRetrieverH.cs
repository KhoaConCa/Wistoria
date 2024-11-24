using UnityEngine;

public class DocumentDataRetrieverH : MonoBehaviour, IDocumentRetriever
{
    private IDocumentDataEditor _documentDetailView;

    private void Start()
    {
        _documentDetailView = GetComponent<IDocumentDataEditor>();
        if (_documentDetailView == null)
        {
            Debug.LogError("IDocumentDataEditor is not attached to this GameObject!");
        }
    }

    public void RetrieveDocumentData()
    {
        if (_documentDetailView != null)
        {
            DocumentDetailD documentData = _documentDetailView.GetEditedDocumentData();

            // Log the retrieved data
            Debug.Log($"Paper Size: {documentData.PaperSize}");
            Debug.Log($"Paper Type: {documentData.PaperType}");
            Debug.Log($"Page Orientation: {documentData.PageOrientation}");
            Debug.Log($"Use Default Pages: {documentData.UseDefaultPages}");
            Debug.Log($"Custom Pages: {documentData.CustomPages}");
            Debug.Log($"No Copies: {documentData.NoCopies}");
            Debug.Log($"Custom Copies: {documentData.CustomCopies}");
        }
        else
        {
            Debug.LogError("IDocumentDataEditor instance is missing!");
        }
    }
}
