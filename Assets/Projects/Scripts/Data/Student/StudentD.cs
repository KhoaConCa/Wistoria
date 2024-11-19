using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StudentD 
{
    #region -- Properties --

    public string _id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string DateOfBirth { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }  
    public string Class {  get; set; }
    public string Course { get; set; }
    public string Paper { get; set; }
    public string Status { get; set; }
    public string createdAt { get; set; }
    public string updatedAt { get; set; }
    public string __v { get; set; }

    #endregion
}
