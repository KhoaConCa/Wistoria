using System.Collections.Generic;
using UnityEngine;

public class HistoryContainerDStudent : MonoBehaviour, IHistoryContainerDStudent
{
    #region -- Implements --

    public void Initialize(string date, List<HistoryDStudent> histories)
    {
        Date = date;
        Histories = histories;
    }

    public string Date { get; set; }
    public List<HistoryDStudent> Histories { get; set; }

    #endregion
}
