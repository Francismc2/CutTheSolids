using UnityEngine;
using XNode;

namespace Engage.AFX.v1.Player.v1
{
    [CreateNodeMenu(AFXMenuTree.Player + "Player Info")]
    public class PlayerInfo : AFXNode, IDelayUntilReady
    {
        [SerializeField] [Output] private GameObject playerGameObjectOut;
        [SerializeField] [Output] private string displayNameOut;
        [SerializeField] [Output] private int playerHeight;
        [SerializeField] [Output] private bool isSessionHost;
        [SerializeField] [Output] private int id;

        public override object GetValue(NodePort port)
        {
            return null;
        }

        bool IDelayUntilReady.IsValueReady()
        {
            return true;
        }
    }
}