using System;
using System.Collections;
using System.Collections.Generic;

public interface IFindable
{
    IEnumerator GetCampus(string campusName, Action<CampusD> onCampusFound);
}
