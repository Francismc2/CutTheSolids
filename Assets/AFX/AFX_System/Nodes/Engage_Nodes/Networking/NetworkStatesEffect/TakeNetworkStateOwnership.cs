using UnityEngine;
using Engage.IFX.NetworkStates;

namespace Engage.AFX.v1
{
    [NodeTitle("Take Network Ownership")]
    [CreateNodeMenu(AFXMenuTree.NetworkStateModules + "Take NetworkState Ownership")]
    public class TakeNetworkStateOwnership : AFXActiveNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never)] private NetworkStateModule module;
        [Header("-1 = local player")]
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private int playerID = -1;

        void SetOwnership()
        {
            module = GetInputValue(nameof(module), module);
            playerID = GetInputValue(nameof(playerID), playerID);
            if (playerID == -1)
            {
                module.SetFixedOwnerMe();
                
            }
            else
            {
                module.SetFixedOwnership(playerID);
            }
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            SetOwnership();
            base.ExecuteNode(exit);
        }
    }
}