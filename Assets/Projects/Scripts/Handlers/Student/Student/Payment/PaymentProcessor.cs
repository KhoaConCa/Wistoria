/*using System.IO;
using UnityEngine;

public class PaymentProcessor : IPaymentProcessor
{
    private readonly IStudentUpdater _studentUpdater;

    public PaymentProcessor(IStudentUpdater studentUpdater)
    {
        _studentUpdater = studentUpdater;
    }

    public void ProcessPayment(PackageD package, string studentId)
    {
        // Tạo dữ liệu hóa đơn thanh toán
        PaymentD payment = new PaymentD
        {
            Paper = package.Paper,
            Person = studentId,
            Status = "Completed"
        };

        // Lưu hóa đơn vào JSON
        string json = JsonUtility.ToJson(payment, true);
        string path = $"{Application.persistentDataPath}/Payment.json";
        File.WriteAllText(path, json);

        Debug.Log($"Payment data saved: {json}");

        // Cập nhật số giấy của sinh viên
        _studentUpdater.UpdateStudentPaperCount(studentId, int.Parse(package.Paper));
    }
}
*/