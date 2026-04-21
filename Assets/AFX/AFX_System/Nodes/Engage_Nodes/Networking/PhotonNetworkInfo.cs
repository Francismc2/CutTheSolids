using XNode;
using UnityEngine;

namespace Engage.AFX.v1
{
    [NodeTitle("PhotonNetwork Info")]
    [CreateNodeMenu(AFXMenuTree.Networking+"PhotonNetwork Info")]
    public class PhotonNetworkInfo : AFXNode
    {
        [SerializeField]
        [Output] private bool isMasterClient;
        [SerializeField]
        [Output] private int playerCount;
        [SerializeField]
        [Output] private float networkTime;
        [SerializeField]
        [Output] private int localPlayerId;
        [SerializeField]
        [Output] private int masterClientId;

        public override object GetValue(NodePort port)
        {
            return null;
        }
    }
}