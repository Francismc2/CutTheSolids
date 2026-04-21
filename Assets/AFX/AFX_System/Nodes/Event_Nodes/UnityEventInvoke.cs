using UnityEngine;

namespace Engage.AFX.v1
{
    [NodeTitle("Invoke UnityEvent")]
    [CreateNodeMenu(AFXMenuTree.Events + "Invoke UnityEvent")]
    public class UnityEventInvoke : AFXActiveNode
    {
        [SerializeField]
        [Input] private UnityEventComponent unityEvent;

        void InvokeEvent()
        {
            unityEvent = GetInputValue(nameof(unityEvent), unityEvent);
            unityEvent.Value.Invoke();
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            InvokeEvent();
            exit.ActivateNextNode(GetPort(nameof(exit)));
        }
    }
}