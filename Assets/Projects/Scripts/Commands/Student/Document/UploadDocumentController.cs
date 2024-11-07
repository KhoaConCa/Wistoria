using UnityEngine;
using UnityEngine.UI;

public class UploadDocumentController : MonoBehaviour
{
    public Button uploadButton; // Tham chiếu đến btnUpLoad
    private IUploadDocumentCommand _uploadCommand;
    private string _selectedFilePath;

    private void Start()
    {
        var handler = gameObject.AddComponent<UploadDocumentH>(); // Sử dụng AddComponent thay vì new
        _selectedFilePath = Application.dataPath + "/Sample.txt"; // Đường dẫn ví dụ

        // Tạo command và khởi tạo với handler và đường dẫn tài liệu
        var uploadCommandComponent = gameObject.AddComponent<UploadDocumentC>();
        uploadCommandComponent.Initialize(handler, _selectedFilePath);
        _uploadCommand = uploadCommandComponent;

        // Kiểm tra nếu uploadButton đã được gán qua Inspector
        if (uploadButton != null)
        {
            uploadButton.onClick.AddListener(OnUploadButtonClicked);
        }
        else
        {
            Debug.LogError("Upload button not assigned in the Inspector.");
        }
    }

    private void OnUploadButtonClicked()
    {
        if (!string.IsNullOrEmpty(_selectedFilePath))
        {
            gameObject.SetActive(true); // Đảm bảo GameObject đang hoạt động
            _uploadCommand.Execute();
        }
    }
}
