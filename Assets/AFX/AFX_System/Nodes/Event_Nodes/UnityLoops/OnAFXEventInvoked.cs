using UnityEngine;

namespace Engage.AFX.v1
{
    [NodeTitle("On AFXEvent Invoked")]
    [CreateNodeMenu(AFXMenuTree.Events + "On AFXEvent Invoked")]
    public class OnAFXEventInvoked : AFXEventNode
    {
        [SerializeField]
        private AFXEvent afxEvent;

        protected override void Init()
        {
            if (!Application.isPlaying) return;
            afxEvent.AddListener(this);
        }

        private void OnDestroy()
        {
            afxEvent.RemoveListener(this);
        }

        public void ExecuteNode()
        {
            ExecuteNode(exit);
        }
    }
}