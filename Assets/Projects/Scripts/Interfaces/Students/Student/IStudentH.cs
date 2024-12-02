using System;
using System.Collections;

public interface IStudentUpdater
{
    IEnumerator FetchStudentData(string studentId, System.Action<StudentD> onSuccess, System.Action<string> onError);
    IEnumerator UpdateStudentPaper(string studentId, int newPaperCount, System.Action onSuccess, System.Action<string> onError);
    IEnumerator FetchAndIncrementPaper(string studentId, int additionalPaper);
}

public interface IStudentPaper
{
    IEnumerator GetStudentPaper(string studentId, Action<int> onSuccess, Action<string> onFailed);
    IEnumerator UpdateStudentPaper(string studentId, int newPaperCount, Action<string> onSuccess, Action<string> onError);
}