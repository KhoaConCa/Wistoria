using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPrinterDocView
{
    void TopUpPaper(int paperNeed);
    void SwitchHistory();
}
