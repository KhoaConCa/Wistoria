using UnityEngine;

public class PrinterDocCardData : MonoBehaviour, IPrinterDocData
{
    #region -- Implements --

    public void Initialize(QueueD queue)
    {
        Queue = queue;
    }

    #region -- Properties --
    public QueueD Queue { get; set; }
    #endregion

    #endregion
}