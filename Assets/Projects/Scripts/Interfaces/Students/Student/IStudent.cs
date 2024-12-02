using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public interface IGetStudentHandler 
{
    IEnumerator GetStudentByID(string id, Action<StudentD> onSuccess, Action<string> onFailed);
}

public interface IStudentView
{
    void SetStudentData(StudentD student);
}