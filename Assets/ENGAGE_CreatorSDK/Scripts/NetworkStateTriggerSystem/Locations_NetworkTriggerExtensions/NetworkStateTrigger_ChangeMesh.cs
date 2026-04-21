using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script to Change an Objects Mesh
/// </summary>

[RequireComponent(typeof(LVR_Location_NetworkState))]
public class NetworkStateTrigger_ChangeMesh : MonoBehaviour {

    [Header("List of Meshes")]
    public List<Mesh> meshs = new List<Mesh>();

    [Header("Gameobject to Change")]
    public GameObject go;

    void Start () 
    {
    }

    public void SetState(int state)
    {
    }

    void ObjectUpdate()
    {
    }

    private void Update()
    {
    }
}
