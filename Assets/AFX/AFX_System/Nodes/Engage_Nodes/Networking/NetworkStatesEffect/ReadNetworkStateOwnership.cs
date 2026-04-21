using UnityEngine;
using XNode;
using Engage.IFX.NetworkStates;

namespace Engage.AFX.v1
{
    [NodeTitle("Read Network Ownership")]
    [CreateNodeMenu(AFXMenuTree.NetworkStateModules + "Read NetworkState Ownership")]
    public class ReadNetworkStateOwnership : AFXNode
    {
        [SerializeField] [Input(ShowBackingValue.Never)] private NetworkStateModule module;
        [Header("-1 = local player")]
        [SerializeField] [Input(ShowBackingValue.Unconnected)] private int playerID = -1;

        [SerializeField] [Output] private bool playerIsOwner;
        [SerializeField] [Output] private bool playerCanControl;
        [SerializeField] [Output] private float serializationRate;

        public override object GetValue(NodePort port)
        {
            module = GetInputValue(nameof(module), module);
            playerID = GetInputValue(nameof(playerID), playerID);

            return null;
        }
    }
}