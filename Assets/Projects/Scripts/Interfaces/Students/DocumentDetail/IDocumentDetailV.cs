using System.Collections.Generic;
using TMPro;

public interface IDocumentDisplay
{
    void DisplayDocumentProperties(DocumentDetailD documentData);
}

public interface IDocumentDataEditor
{
    DocumentDetailD GetEditedDocumentData();
}

public interface IDropdownInitializer
{
    void InitializeDropdown(TMP_Dropdown dropdown, List<string> options);
}
