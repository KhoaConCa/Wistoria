using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintH : MonoBehaviour, ILayout
{
    #region -- Implements --

    public void IButton(GameObject childNext)
    {
        foreach (var item in listPrint)
            if (item.activeSelf)
                childCurrent = item;

        childNext.SetActive(true);
        childCurrent.SetActive(false);
    }

    public void OnDisable()
    {
        for (int i = 0; i < listPrint.Count; i++)
        {
            if (i == 0)
                listPrint[i].SetActive(true);
            else
                listPrint[i].SetActive(false);
        }
    }

    #endregion

    #region -- Fields --

    [SerializeField] private List<GameObject> listPrint = new List<GameObject>();
    private GameObject childCurrent;

    #endregion
}
