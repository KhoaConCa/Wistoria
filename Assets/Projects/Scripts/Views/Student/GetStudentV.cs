using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GetStudentV : MonoBehaviour, IStudentView
{
    #region -- Implements --

    /*    public void DisplayCampus(List<CampusD> campuses)
        {
            if (campuses.Count > 0)
            {
                var campus = campuses[0];
                campusName.text = campus.CampusName;
                campusRoom.text = campus.Room;
            }
        }*/

    public void SetStudentData(StudentD student)
    {
        studentFirstName.text = student.FirstName;
        studentLastName.text = student.LastName;
        studentDateofBirth.text = student.DateOfBirth;
        studentPhoneNumber.text = student.PhoneNumber;
        studentEmail.text = student.Email;
        studentClass.text = student.Class;
        studentCourse.text = student.Course;
        studentPaper.text = student.Paper;
        studentStatus.text = student.Status;
    }

    #endregion

    #region -- Fields --

    public TextMeshProUGUI studentFirstName;
    public TextMeshProUGUI studentLastName;
    public TextMeshProUGUI studentDateofBirth;
    public TextMeshProUGUI studentPhoneNumber;
    public TextMeshProUGUI studentEmail;
    public TextMeshProUGUI studentClass;
    public TextMeshProUGUI studentCourse;
    public TextMeshProUGUI studentPaper;
    public TextMeshProUGUI studentStatus;


    #endregion
}
