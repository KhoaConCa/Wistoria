using UnityEngine;

public interface ITransformUI
{
    void SetActiveObjectUI(GameObject targetObject);
    void SetActiveObjectUI(string targetTag);
}

public interface IStudentPrinterTransformUI
{
    void SetActiveObjectUI(GameObject targetObject);
    void OnUploadToProperty();
    void SetActiveObjectUI(string targetTag);

}