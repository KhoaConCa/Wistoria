using System;
using System.Collections;



public interface IStudentPaper
{
    IEnumerator GetStudentPaper(string studentId, Action<int> onSuccess, Action<string> onFailed);
    IEnumerator UpdateStudentPaper(string studentId, int newPaperCount, Action<string> onSuccess, Action<string> onError);
}