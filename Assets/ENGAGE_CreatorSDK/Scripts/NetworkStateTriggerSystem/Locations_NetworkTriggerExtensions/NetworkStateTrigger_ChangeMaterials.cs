using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script to Change Mesh Renderer(s) Material at runtime
/// </summary>

[RequireComponent(typeof(LVR_Location_NetworkState))]
public class NetworkStateTrigger_ChangeMaterials : MonoBehaviour
{
    [Header("List of Switchable Materials")]
    [SerializeField] private List<Material> materialList = new List<Material>();

    [Header("List of Mesh Renderers")]
    [SerializeField] private List<SwitchRenderer> switchRenderers = new List<SwitchRenderer>();

    private LVR_Location_NetworkState state;
    private int lastState;
    private int storedState;

    [System.Serializable]
    public class SwitchRenderer
    {
        [Header("Mesh Renderer to Change")]
        [SerializeField] private MeshRenderer meshRenderer;

        [Header("Material Index to Change (Default zero unless multiple materials)")]
        [SerializeField] private int materialIndex;

        public MeshRenderer MeshRenderer { get => meshRenderer; set => meshRenderer = value; }
        public int MaterialIndex { get => materialIndex; set => materialIndex = value; }
    }

    void Start()
    {
        state = GetComponent<LVR_Location_NetworkState>();
        storedState = state.currentState;
    }

    /// Set material
    public void SetState(int state)
    {
        this.state.UpdateState(state);
    }

    /// Set next state (next material in material list)
    public void NextState()
    {
        if (state.currentState < materialList.Count - 1)
        {
            state.UpdateState(state.currentState + 1);
        }
        else
        {
            state.UpdateState(0);
        }
    }

    /// Set previous state (previous material in material list)
    public void PreviousState()
    {
        if (state.currentState > 0)
        {
            state.UpdateState(state.currentState - 1);
        }
        else
        {
            state.UpdateState(materialList.Count - 1);
        }
    }

    /// Toggle previous state (last set material)
    public void TogglePreviousState()
    {
        state.UpdateState(lastState);
    }

    /// Update states / stored values
    private void ObjectUpdate()
    {
        if (state.currentState >= materialList.Count)
            return;

        foreach (SwitchRenderer renderer in switchRenderers)
        {
            Material[] CopyMaterials = renderer.MeshRenderer.materials;
            CopyMaterials[renderer.MaterialIndex] = materialList[state.currentState];
            renderer.MeshRenderer.materials = CopyMaterials;
        }

        lastState = storedState;
        storedState = state.currentState;
    }

    private void Update()
    {
        if (state.currentState != storedState)
        {
            ObjectUpdate();
        }
    }
}
