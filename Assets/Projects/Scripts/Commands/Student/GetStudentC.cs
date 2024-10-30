using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class GetStudentC : MonoBehaviour
{
    #region -- Implements --

    /// <summary>
    /// Send a get request to server when clicked
    /// Display information of the user
    /// </summary>

    public void ClickInfoButton()
    {
        string studentID = GetStudentID();
        StartCoroutine(_studentHandler.GetStudent(studentID));
    }

    /// <summary>
    /// ID user
    /// </summary>
    /// <returns> test user</returns>

    public string GetStudentID()
    {
        return "671860901e0844975517030e";
    }

    /// <summary>
    /// Get component and add listener
    /// </summary>

    public void Initialization()
    {
        if (_studentHandler == null)
        {
            _studentHandler = gameObject.AddComponent<GetStudentH>();
        }

        getButton.onClick.AddListener(ClickInfoButton);
    }

    #endregion

    #region -- Methods --

    void Start()
    {
        Initialization();
    }

    void SetStudentFirstName(string input)
    {
        studentFirstName.text = input;
    }

    void SetStudentLastName(string input)
    {
        studentLastName.text = input;
    }

    void SetStudentDateOfBirth(string input)
    {
        studentDateOfBirth.text = input;
    }

    void SetStudentPhoneNumber(string input)
    {
        studentPhoneNumber.text = input;
    }

    void SetStudentEmail(string input)
    {
        studentEmail.text = input;
    }

    void SetStudentClass(string input)
    {
        studentClass.text = input;
    }

    void SetStudentCourse(string input)
    {
        studentCourse.text = input;
    }

    void SetStudentPaper(string input)
    {
        studentPaper.text = input;
    }

    void SetStudentStatus(string input)
    {
        studentStatus.text = input;
    }

    #endregion

    #region -- Fields --

    public TextMeshProUGUI studentFirstName;
    public TextMeshProUGUI studentLastName;
    public TextMeshProUGUI studentDateOfBirth;
    public TextMeshProUGUI studentPhoneNumber;
    public TextMeshProUGUI studentEmail;
    public TextMeshProUGUI studentClass;
    public TextMeshProUGUI studentCourse;
    public TextMeshProUGUI studentPaper;
    public TextMeshProUGUI studentStatus;

    public Button getButton;

    public IGetStudentHandler _studentHandler;

    #endregion
}
