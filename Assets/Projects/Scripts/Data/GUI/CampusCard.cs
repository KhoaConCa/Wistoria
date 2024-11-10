using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CampusCard : MonoBehaviour, ICampusCardClicker
{
    #region -- Methods --

    /// <summary>
    /// 
    /// </summary>
    /// <param name="campus"></param>
    /// <param name="onClickCallback"></param>
    public void Initialize(string campusID, Action<string> onClickCallback)
    {
        _campusId = campusID;
        _onClickCallback = onClickCallback;
    }

    private void OnCardClicked()
    {
        _onClickCallback?.Invoke(_campusId);
    }

    #endregion

    #region -- Fields --

    private string _campusId;

    private Action<string> _onClickCallback;

    #endregion
}
