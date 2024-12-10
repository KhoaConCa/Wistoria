using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IQueueV
{
    void CheckingQueue();
    void SetNewQueue(QueueD queue);
    bool IsChecking { get; set; }
}
