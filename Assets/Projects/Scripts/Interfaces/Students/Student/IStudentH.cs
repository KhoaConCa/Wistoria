using System.Collections;

public interface IStudentUpdater
{
    IEnumerator FetchStudentData(string studentId, System.Action<StudentD> onSuccess, System.Action<string> onError);

    IEnumerator UpdateStudentPaper(string studentId, int newPaperCount, System.Action onSuccess, System.Action<string> onError);
    IEnumerator FetchAndIncrementPaper(string studentId, int additionalPaper);


}


