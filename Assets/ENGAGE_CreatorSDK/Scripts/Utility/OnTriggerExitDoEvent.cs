using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTriggerExitDoEvent : MonoBehaviour
{
    public LayerMask layerMask;
    public UnityEvent eventToInvoke;
    public string optional_objectNameToExitContains;


    private void OnTriggerExit(Collider other)
    {
        if (layerMask == (layerMask | (1 << other.gameObject.layer)))
        {
            if(string.IsNullOrEmpty(optional_objectNameToExitContains) || other.gameObject.name.Contains(optional_objectNameToExitContains))
                eventToInvoke?.Invoke();
        }
    }
}
