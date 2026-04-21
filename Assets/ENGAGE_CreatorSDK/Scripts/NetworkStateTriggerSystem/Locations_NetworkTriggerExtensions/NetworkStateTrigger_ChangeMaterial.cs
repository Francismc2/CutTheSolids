using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script to Change an Objects Material
/// </summary>

[RequireComponent(typeof(LVR_Location_NetworkState))]
public class NetworkStateTrigger_ChangeMaterial : MonoBehaviour {

    [Header("List of Materials")]
    public List<Material> materials = new List<Material>();

    [Header("Gameobject to Change")]
    public GameObject go;


    void Start ()
    {
    }

    public void SetState(int state)
    {
    }

    private void Update()
    {
    }
}
