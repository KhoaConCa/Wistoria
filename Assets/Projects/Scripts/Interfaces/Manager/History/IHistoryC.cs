using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public interface IGetContainerHistoryCommand
{
    void OnHistoryFound(string dateTime, List<HistoryD> histories);
}
