
using System.Collections.Generic;
using UnityEngine;
namespace Engage.IFX.NetworkStates
{
    /// <summary>
    /// Initializer for unique identification of Network State Modules in IFX system.
    /// Root component required for using network states in IFX.
    /// </summary>
    public class IFXNetworkStatesEffect : MonoBehaviour
    {
        [Header("This MUST be on the root object of any IFX using Network State Modules!", order = 0)]
        [Header("List all network state modules within this IFX (and child hierarchy) here.", order = 2)]
        [Header("Once a network state module is added to an IFX, do not change its position in the list. Leave null after deleting.", order = 3)]
        [Header("This is to ensure recorded states are properly maintained over time.", order = 4)]
        [Header("",order = 5)]
        [Header("", order = 6)]

        [SerializeField]
        private List<NetworkStateModule> networkStateModules = new List<NetworkStateModule>();

    }
}
