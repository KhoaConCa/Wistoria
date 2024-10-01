using SaiHttpRequest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class JsonBinApiCall : ApiCall
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override string Domain()
    {
        return "api.jsonbin.io/v3";
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    public override void SetHeaders(UnityWebRequest request)
    {
        base.SetHeaders(request);

        //TODO: set your own Master Key here
        request.SetRequestHeader("X-Master-Key", "$2b$10$pptPZOSqaNmO1iJ9V/5dEudIXNXAUHIw8MVVKyKmmtyA0OdL27ALO");
    }
}
