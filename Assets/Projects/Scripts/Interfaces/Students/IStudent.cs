using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public interface IGetStudentHandler 
{
    IEnumerator GetStudent(string studentID);
}

public interface IStudentView
{
    void SetStudentData(StudentD student);
}