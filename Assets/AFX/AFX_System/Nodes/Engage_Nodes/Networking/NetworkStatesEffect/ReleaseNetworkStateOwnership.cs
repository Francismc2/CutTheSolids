using UnityEngine;
using Engage.IFX.NetworkStates;
namespace Engage.AFX.v1
{
    [NodeTitle("Release Network Ownership")]
    [CreateNodeMenu(AFXMenuTree.NetworkStateModules + "Release NetworkState Ownership")]
    public class ReleaseNetworkStateOwnership : AFXActiveNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never)] private NetworkStateModule module;

        void ReleaseOwnership()
        {
            module = GetInputValue(nameof(module), module);
            module.ReleaseFixedOwnership();
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            ReleaseOwnership();
            base.ExecuteNode(exit);
        }
    }
}