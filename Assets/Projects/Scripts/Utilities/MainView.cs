using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public static class MainView
    {
        public static Transform FindObjectsByTag(Transform parentObject, string tagName)
        {
            if (parentObject.tag == tagName)
                return parentObject;

            for (int i = 0; i < parentObject.transform.childCount; i++)
            {
                Transform result = FindObjectsByTag(parentObject.transform.GetChild(i), tagName);

                if (result != null)
                    return result;
            }

            return null;
        }

        public static void OnSuccess(string message)
        {
            Debug.Log(message);
        }

        public static void OnFaild(string message)
        {
            Debug.Log(message);
        }
    }
}

