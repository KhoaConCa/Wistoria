using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public static class MainView
    {
        public static Transform FindChildObjectsByTag(Transform parentObject, string tagName)
        {
            if (parentObject.tag == tagName)
                return parentObject;

            for (int i = 0; i < parentObject.transform.childCount; i++)
            {
                Transform result = FindChildObjectsByTag(parentObject.transform.GetChild(i), tagName);

                if (result != null)
                    return result;
            }

            return null;
        }
    }
}

