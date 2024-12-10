using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HistoryContainerData : MonoBehaviour, IHistoryContainerData
{
    #region -- Implements --

    public void Initialize(string date, List<HistoryD> histories)
    {
        Date = date;
        Histories = histories;
    }

    public string Date { get; set; }
    public List<HistoryD> Histories { get; set; }

    #endregion
}
