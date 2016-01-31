using UnityEngine;
using System.Collections;

public static class ExtensionMethods
{

    public static void DestroyRecursive(this GameObject o, float delay = 0)
    {
        var childCount = o.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            o.transform.GetChild(i).gameObject.DestroyRecursive(delay);
        }

        if (delay > 0)
        {
            Object.Destroy(o, delay);
        }
        else
        {
            Object.Destroy(o);
        }
    }

}
