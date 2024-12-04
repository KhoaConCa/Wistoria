using System.Collections.Generic;
using TMPro;

public interface IDocumentDataEditor
{
    DocumentDetailD GetEditedDocumentData();
    void DisplayDocumentProperties(DocumentDetailD documentData);

}

public interface IDropdownInitializer
{
    void InitializeDropdown(TMP_Dropdown dropdown, List<string> options);
}
public interface IDocumentDetailRetriever
{
    /// <summary>
    /// Retrieves the document data from the UI.
    /// </summary>
    /// <returns>A `DocumentDetailD` object containing the document data.</returns>
    DocumentDetailD GetDocumentData();

    /// <summary>
    /// Updates the document data on the UI.
    /// </summary>
    /// <param name="data">The new document data to display.</param>
    void UpdateDocumentData(DocumentDetailD data);
}
public interface IDocumentRetriever
{
    void RetrieveDocumentData();
}


