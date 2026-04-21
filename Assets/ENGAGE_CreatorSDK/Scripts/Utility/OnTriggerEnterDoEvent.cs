using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTriggerEnterDoEvent : MonoBehaviour
{
    public LayerMask layerMask;
    public UnityEvent eventToInvoke;
    public string optional_objectNameToEnterContains;

    private void OnTriggerEnter(Collider other)
    {
        if (layerMask == (layerMask | (1 << other.gameObject.layer)))
        {
            if(eventToInvoke != null)
                eventToInvoke.Invoke();
        }
    }
}
