using UnityEngine;

namespace Engage.IFX.NetworkObjects
{
    public class IFXNetworkPhysicsObject : MonoBehaviour
    {
        [Header("", order = 0)]
        [Header("Create Networked Object for IFX", order = 1)]
        [Header("Do not use Mesh Colliders", order = 2)]
        [Header("Make this parent object", order = 3)]
        [Header("This object's scale will always be (1,1,1)", order = 4)]
        [Header("", order = 5)]

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
        /// <summary> No real need to do this, unless trying to collide with local player body</summary>
        [SerializeField] private bool dontChangeLayer;
    }
}