using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPrinterDocData
{
    QueueD Queue { get; set; }
    void Initialize(QueueD queue);
}
