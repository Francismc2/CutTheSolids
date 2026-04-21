using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Do event on enable and/or disable (highly customizable)
/// </summary>
public class OnEnableDoEvent : MonoBehaviour
{
    public float onEnableEventDelaySeconds = 0;
    public UnityEvent eventOnEnable;
    public UnityEvent eventOnDisable;

    void OnEnable()
    {
        if (onEnableEventDelaySeconds > 0)
            Invoke("GoOnEnableEvent", onEnableEventDelaySeconds);
        else
            GoOnEnableEvent();
    }

    void GoOnEnableEvent()
    {
        eventOnEnable.Invoke();
    }

    void OnDisable()
    {
        eventOnDisable.Invoke();
    }
}
