using UnityEngine;
using UnityEngine.UI;

public class CreatePaymentC : MonoBehaviour
{
    #region -- Implements --

    public void UploadPaymentInformation()
    {
        _createPayment = gameObject.AddComponent<CreatePaymentH>();

        PaymentD payment = new PaymentD
        {
            Paper = "50",
            Person = MainUser.STUDENT_ID,
            Status = "Finished"
        };

        StartCoroutine(_createPayment.Upload(payment, onSuccess: response => Debug.Log($"Success: {response}"),
            onFaild: error => Debug.LogError($"Failed: {error}")));

    }

    #endregion

    #region -- Fields --

    public Button getButton;

    private ICreatePaymentHandler _createPayment;

    #endregion 
}
