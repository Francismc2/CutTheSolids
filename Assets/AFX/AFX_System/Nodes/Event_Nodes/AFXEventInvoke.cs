using UnityEngine;

namespace Engage.AFX.v1
{
    [NodeTitle("Invoke AFXEvent")]
    [CreateNodeMenu(AFXMenuTree.EventsInvoke + "Invoke AFXEvent")]
    public class AFXEventInvoke : AFXActiveNode
    {
        [SerializeField]
        private AFXEvent afxEvent;

        void InvokeEvent()
        {
            if (!Application.isPlaying) return;

            afxEvent = GetInputValue(nameof(afxEvent), afxEvent);
            afxEvent.TriggerEvent();
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            InvokeEvent();
            base.ExecuteNode(exit);
        }
    }
}