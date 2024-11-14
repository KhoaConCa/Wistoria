/*using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using System.Threading.Tasks;

public class StudentUpdater : IStudentUpdater
{
    private readonly string apiUrl = "https://your-api-endpoint.com/api/student";
    private readonly string studentId = "your-fixed-student-id"; // Gắn sẵn studentId

    public async void UpdateStudentPaperCount(int paperCount)
    {
        // Lấy thông tin sinh viên từ API
        StudentD student = await FetchStudentData();

        if (student == null)
        {
            Debug.LogError($"Student with ID {studentId} not found!");
            return;
        }

        // Cập nhật số giấy
        int currentPaper = int.Parse(student.Paper);
        student.Paper = (currentPaper + paperCount).ToString();

        // Lưu lại vào JSON
        string json = JsonUtility.ToJson(student, true);
        string path = $"{Application.persistentDataPath}/Student.json";
        File.WriteAllText(path, json);

        Debug.Log($"Student data updated: {json}");

        // Đẩy dữ liệu đã cập nhật lên API (nếu cần)
        await PushUpdatedStudentData(student);
    }

    private async Task<StudentD> FetchStudentData()
    {
        string url = $"{apiUrl}/{studentId}";

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            request.SetRequestHeader("Content-Type", "application/json");

            var operation = request.SendWebRequest();
            while (!operation.isDone)
            {
                await Task.Yield();
            }

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log($"Student data fetched: {request.downloadHandler.text}");
                return JsonUtility.FromJson<StudentD>(request.downloadHandler.text);
            }
            else
            {
                Debug.LogError($"Error fetching student data: {request.error}");
                return null;
            }
        }
    }

    private async Task PushUpdatedStudentData(StudentD student)
    {
        string url = $"{apiUrl}/{student.StudentId}";
        string json = JsonUtility.ToJson(student);

        using (UnityWebRequest request = UnityWebRequest.Put(url, json))
        {
            request.SetRequestHeader("Content-Type", "application/json");

            var operation = request.SendWebRequest();
            while (!operation.isDone)
            {
                await Task.Yield();
            }

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Student data updated successfully on the server.");
            }
            else
            {
                Debug.LogError($"Error updating student data: {request.error}");
            }
        }
    }
}


*/