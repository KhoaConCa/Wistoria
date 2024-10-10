using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayoutH : MonoBehaviour
{
    #region -- Methods --

    private void Awake()
    {
        layoutD = layoutMiddle.GetComponent<LayoutD>();
    }

    public void ButtonController(GameObject layout)
    {
        foreach (var item in layoutD.Layout)
        {
            if (layout.name == item.name && item.activeSelf) return;

            item.SetActive(false);
        }

        layout.SetActive(true);
    }

    public void LabelController()
    {
        
    }

    #endregion

    #region -- Fields --

    [SerializeField] private GameObject layoutMiddle;
    private LayoutD layoutD;

    #endregion
}
