using UnityEngine;

public interface ITransformUI
{
    void SetActiveObjectUI(GameObject targetObject);
    void SetActiveObjectUI(string targetTag);
}