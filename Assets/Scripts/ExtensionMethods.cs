using UnityEngine;
using System.Collections;

public static class ExtensionMethods
{

    public static void DestroyRecursive(this GameObject o)
    {
        var childCount = o.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            o.transform.GetChild(i).gameObject.DestroyRecursive();
        }
        Object.Destroy(o);
    }

}
