using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using Utilities;

public class StudentUpdater : MonoBehaviour, IStudentUpdater
{
    private const string BaseUrl = "https://server-wistoria-api.vercel.app/student/";

    /// <summary>
    /// Fetches the student data from the server by student ID.
    /// </summary>
    /// <param name="studentId">The ID of the student.</param>
    /// <param name="onSuccess">Callback for success, passing the fetched student data.</param>
    /// <param name="onError">Callback for failure, passing the error message.</param>
    public IEnumerator FetchStudentData(string studentId, System.Action<StudentD> onSuccess, System.Action<string> onError)
    {
        if (string.IsNullOrEmpty(studentId))
        {
            onError?.Invoke("Student ID is null or empty. Cannot fetch student data.");
            yield break;
        }

        string fetchUrl = $"{BaseUrl}search/id?id={studentId}";
        UnityWebRequest fetchRequest = UnityWebRequest.Get(fetchUrl);

        yield return fetchRequest.SendWebRequest();

        if (fetchRequest.result == UnityWebRequest.Result.Success)
        {
            string jsonResponse = fetchRequest.downloadHandler.text;
            Debug.Log($"Fetched student data: {jsonResponse}");

            StudentD student = MainHandler.FromJson<StudentD>(jsonResponse)[0];

            if (student != null)
            {
                onSuccess?.Invoke(student);
            }
            else
            {
                onError?.Invoke("Failed to parse student data.");
            }
        }
        else
        {
            onError?.Invoke($"Failed to fetch student data: {fetchRequest.error}");
            Debug.LogError($"Response: {fetchRequest.downloadHandler.text}");
        }
    }

    /// <summary>
    /// Updates the student's paper count on the server.
    /// </summary>
    /// <param name="studentId">The student ID to update.</param>
    /// <param name="newPaperCount">The new paper count.</param>
    /// <param name="onSuccess">Callback for success.</param>
    /// <param name="onError">Callback for failure, passing the error message.</param>
    public IEnumerator UpdateStudentPaper(string studentId, int newPaperCount, System.Action onSuccess, System.Action<string> onError)
    {
        if (string.IsNullOrEmpty(studentId))
        {
            onError?.Invoke("Student ID is null or empty. Cannot update paper count.");
            yield break;
        }

        // Construct the payload for the paper update
        var payload = new { paper = newPaperCount };
        string updatedJson = JsonConvert.SerializeObject(payload);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(updatedJson);

        string updateUrl = $"{BaseUrl}update/paper?id={studentId}";
        UnityWebRequest patchRequest = new UnityWebRequest(updateUrl, "PATCH")
        {
            uploadHandler = new UploadHandlerRaw(bodyRaw),
            downloadHandler = new DownloadHandlerBuffer()
        };
        patchRequest.SetRequestHeader("Content-Type", "application/json");

        yield return patchRequest.SendWebRequest();

        if (patchRequest.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Student paper count updated successfully.");
            Debug.Log($"Response: {patchRequest.downloadHandler.text}");
            onSuccess?.Invoke();
        }
        else
        {
            onError?.Invoke($"Failed to update student data: {patchRequest.error}");
            Debug.LogError($"Response: {patchRequest.downloadHandler.text}");
        }
    }

    /// <summary>
    /// Fetches the current student data, increments the paper count, and updates the server.
    /// </summary>
    /// <param name="studentId">The student ID to fetch and update.</param>
    /// <param name="additionalPaper">The additional paper count to add.</param>
    /// <returns>An IEnumerator for coroutine usage.</returns>
    public IEnumerator FetchAndIncrementPaper(string studentId, int additionalPaper)
    {
        yield return FetchStudentData(studentId,
            student =>
            {
                int currentPaperCount = int.Parse(student.Paper);
                int updatedPaperCount = currentPaperCount + additionalPaper;

                StartCoroutine(UpdateStudentPaper(studentId, updatedPaperCount,
                    () => Debug.Log("Student paper count successfully incremented."),
                    error => Debug.LogError(error)));
            },
            error => Debug.LogError(error));
    }
}
