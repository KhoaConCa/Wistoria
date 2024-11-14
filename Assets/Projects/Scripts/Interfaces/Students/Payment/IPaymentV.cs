public interface IPaymentView
{
    /// <summary>
    /// Sets payment data in the view for display.
    /// </summary>
    /// <param name="payment">The payment data to display.</param>
    void SetPaymentData(PaymentD payment);
}