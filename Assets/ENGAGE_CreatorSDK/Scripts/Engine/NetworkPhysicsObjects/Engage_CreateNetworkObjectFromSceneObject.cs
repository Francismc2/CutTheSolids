using System;
using UnityEngine;
using UnityEngine.Events;

public class Engage_CreateNetworkObjectFromSceneObject : MonoBehaviour
{
    [Serializable] public class IsOwnerEvent : UnityEvent<bool> { }
    [Header("",order = 0)]
    [Header("Create Networked Object for ENGAGE Scene", order = 1)]
    [Header("Do not use Mesh Colliders", order = 2)]
    [Header("Make this parent object", order = 3)]
    [Header("This objects scale will always be (1,1,1)", order = 4)]
    [Header("",order = 5)]

    /// <summary>Unique object name (make sure this is scene-unique)</summary>
    [SerializeField] private string veryUniqueObjectName;

    /// <summary>Enable gravity on rigidbody?</summary>
    [SerializeField] private bool gravityEnabled;

    /// <summary>Should this object always be kinematic? (no physics)</summary>
    [SerializeField] private bool alwaysKinematic;

    /// <summary>String that can be applied and used for interactive games</summary>
    [SerializeField] private string optionalStringForGames;

    /// <summary>Default is to use Bones tag</summary>
    [SerializeField] private bool dontChangeTag;

    /// <summary>Should this object collide with other network objects</summary>
    [SerializeField] private bool allowSelfCollision;

    /// <summary>Allow this object to show up in recordings?</summary>
    [SerializeField] private bool allowRecording = true;

    [Header("Advanced usage only - can cause unintended interactions")]
    /// <summary>No real need to do this, unless trying to collide with local player body</summary>
    [SerializeField] private bool dontChangeLayer;

    /// <summary>If trying to create a non-synced, single-user grabbable physics object</summary>
    [SerializeField] private bool singleUserNoSync;

    /// <summary>Called whenever owner changes, will be true if owner is local</summary>
    [SerializeField] private IsOwnerEvent OnOwnerChangedIsOwner;

    #region Network Object manual interface 

    /// <summary>
    /// Update transform value manually. This should be done rarely (via button / etc) by one user if possible.
    /// Object will be dropped if it is held when function is called.
    /// </summary>
    public void UpdatePositionManualViaReference(Transform positionReferenceTransform){}

    public void UpdateRotationManualViaReference(Transform rotationReferenceTransform){}

    public void UpdateScaleManualViaReference(Transform scaleReferenceTransform){}

    public void UpdateAllTransformValuesManualViaReference(Transform referenceTransform){}

    public void SetKinematicDefault(bool alwaysKinematic){}

    public void SetGravityEnabledDefault(bool gravityEnabled){}

    public void SetIsGrabbable(bool grabbable){}

    #endregion
}
