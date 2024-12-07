using UnityEngine;
using SimpleFileBrowser;
using System.IO;
using Utilities;

public class UploadDocumentController : MonoBehaviour
{
    #region -- Methods --

    private void Awake()
    {
        GetComponent();
        AddComponent();
    }

    #region -- Add Component --

    private void AddComponent()
    {
        if (_upLoadH == null)
            _upLoadH = gameObject.AddComponent<UploadDocumentH>();
    }

    #endregion

    #region -- Get Component --

    private void GetComponent()
    {
        if (_cardData == null)
        {
            GameObject defaultUI = GameObject.FindWithTag("DefaultMainScene");
            _cardData = defaultUI.GetComponent<PrinterDocCard>();
        }

        _documentV = _targetObject.GetComponent<DocumentDetailV>();
    }

    #endregion

    #region -- Update Document Event --

    public void OnUploadButtonClicked()
    {
        FileBrowser.ShowLoadDialog(
            (paths) =>
            {
                _path = paths[0];
                UploadDocument();
            },
            () => Debug.Log("File selection canceled."),
            FileBrowser.PickMode.Files, false, null, "*.pdf,*.doc,*.docx", "Chọn tệp cần in", "Chọn"
        );
    }

    #endregion

    #region -- Get Info Data --

    private void UploadDocument()
    {
        FileInfo fileInfo = new FileInfo(_path);

        StartCoroutine(_upLoadH.GetStudentByID(MainUser.STUDENT_ID, onSuccess =>
        {
            if (onSuccess != null)
            {
                DocumentDStudent document = new DocumentDStudent() 
                { 
                    Student = onSuccess,
                    Name = fileInfo.Name,
                    Size = Random.Range(20, 50)
                };

                _cardData.Document = document;
            }

            UploadDocumentToServer();
        }, onFailed =>
        {
            MainView.OnReset(UploadDocument, onFailed);
        }));
    }

    private void UploadDocumentToServer()
    {
        StartCoroutine(_upLoadH.UploadDocument(_cardData.Document, onSuccess =>
        {
            MainView.OnDebugged(onSuccess);

            _documentV.DefaultDocument();

        }, onFailed =>
        {
            MainView.OnReset(UploadDocumentToServer, onFailed);
        }));
    }
    #endregion

    #endregion

    #region -- Fields --

    private ITransformUI _transform;
    private IDocumentDefailV _documentV;
    private IUploadDocumentCommand _uploadC;
    private IUploadDocumentHandler _upLoadH;

    [SerializeField] private PrinterDocCard _cardData;
    private string _path;

    [SerializeField] private GameObject _targetObject;

    #endregion
}
