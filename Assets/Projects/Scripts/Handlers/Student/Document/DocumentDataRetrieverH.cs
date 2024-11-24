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

            // Populate DocumentService with retrieved data
            DocumentService.PaperSize = documentData.PaperSize;
            DocumentService.Orientation = documentData.PageOrientation;
            DocumentService.Side = documentData.PaperType;
            DocumentService.PageBegin = int.TryParse(documentData.CustomPages.Split('-')[0], out int begin) ? begin : 1;
            DocumentService.PageEnd = int.TryParse(documentData.CustomPages.Split('-')[1], out int end) ? end : 1;
            DocumentService.Copies = documentData.CustomCopies;
            DocumentService.Color = !documentData.NoCopies; // Assuming color is selected when NoCopies is false

            // Log the retrieved data
            Debug.Log($"Paper Size: {documentData.PaperSize}");
            Debug.Log($"Orientation: {documentData.PageOrientation}");
            Debug.Log($"Side: {DocumentService.Side}");
            Debug.Log($"Page Begin: {DocumentService.PageBegin}");
            Debug.Log($"Page End: {DocumentService.PageEnd}");
            Debug.Log($"Copies: {documentData.CustomCopies}");
            Debug.Log($"Color: {DocumentService.Color}");
        }
        else
        {
            Debug.LogError("IDocumentDataEditor instance is missing!");
        }
    }

}
