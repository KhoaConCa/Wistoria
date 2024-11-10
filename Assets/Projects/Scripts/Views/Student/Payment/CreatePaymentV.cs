using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CreatePaymentV : MonoBehaviour, IPaymentView
{
    #region -- Implements --

    public void SetPaymentData(PaymentD payment)
    {
        paymentPaper.text = payment.Paper;
    }

    #endregion

    #region -- Fields --

    public TextMeshProUGUI paymentPaper;

    #endregion
}
