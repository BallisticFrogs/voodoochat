using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class GenericAnimation : MonoBehaviour
{

    public UnityEvent OnBegin;
    public UnityEvent OnEnd;

    public void Begin()
    {
        if (OnBegin != null) OnBegin.Invoke();
    }

    public void End()
    {
        if (OnEnd != null) OnEnd.Invoke();
    }

}
