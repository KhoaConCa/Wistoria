public interface IDocumentInitialization
{
    /// <summary>
    /// Initializes document data with default values and displays it on the view.
    /// </summary>
    void InitializeDocument();
}

public interface IDocumentUpdater
{
    /// <summary>
    /// Updates document properties and reflects changes in the view.
    /// </summary>
    /// <param name="newData">The new data for updating the document properties.</param>
    void UpdateDocumentData(DocumentDetailD newData);
}