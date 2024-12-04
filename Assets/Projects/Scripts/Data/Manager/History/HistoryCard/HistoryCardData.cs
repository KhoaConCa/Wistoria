using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HistoryCardData : MonoBehaviour, IHistoryCardData
{
    public void Initialize(HistoryD history)
    {
        Id = history.Id;
        Name = history.Name;
        StudentName = history.StudentName;
        TypeData = history.TypeData;
        DateProcess = history.DateProcess;
        Paper = history.Paper;
    }

    public string Id { get; set; }
    public string Name { get; set; }
    public string StudentName { get; set; }
    public int TypeData { get; set; }
    public DateTime? DateProcess { get; set; }
    public int Paper { get; set; }
}
