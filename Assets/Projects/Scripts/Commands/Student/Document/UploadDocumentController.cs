using UnityEngine;
using UnityEngine.UI;

#region -- Class Description --
/// <summary>
/// Controller class for managing document upload functionality.
/// Initializes upload command and assigns the upload button's click event.
/// </summary>
#endregion
public class UploadDocumentController : MonoBehaviour
{
    #region -- Unity Methods --

    /// <summary>
    /// Unity's Start method.
    /// Initializes the upload command and sets up the upload button click event.
    /// </summary>
    private void Start()
    {
        var handler = gameObject.AddComponent<UploadDocumentH>(); // Add handler as a component
        _selectedFilePath = Application.dataPath + "/Sample.txt"; // Example file path

        // Create and initialize the upload command with handler and file path
        var uploadCommandComponent = gameObject.AddComponent<UploadDocumentC>();
        uploadCommandComponent.Initialize(handler, _selectedFilePath);
        _uploadCommand = uploadCommandComponent;

        // Check if uploadButton is assigned in the Inspector
        if (uploadButton != null)
        {
            uploadButton.onClick.AddListener(OnUploadButtonClicked);
        }
        else
        {
            Debug.LogError("Upload button not assigned in the Inspector.");
        }
    }

    #endregion

    #region -- Private Methods --

    /// <summary>
    /// Called when the upload button is clicked.
    /// Executes the upload command if a file path is selected.
    /// </summary>
    private void OnUploadButtonClicked()
    {
        if (!string.IsNullOrEmpty(_selectedFilePath))
        {
            gameObject.SetActive(true); // Ensure GameObject is active
            _uploadCommand.Execute();
        }
    }

    #endregion

    #region -- Fields --

    [Header("Upload Button")]
    /// <summary>
    /// Reference to the upload button (should be assigned in the Inspector).
    /// </summary>
    public Button uploadButton;

    private IUploadDocumentCommand _uploadCommand;
    private string _selectedFilePath;

    #endregion
}
