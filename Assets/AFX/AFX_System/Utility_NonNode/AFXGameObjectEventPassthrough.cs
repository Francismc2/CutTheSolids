using UnityEngine;
namespace Engage.AFX.v1
{
    public class AFXGameObjectEventPassthrough : MonoBehaviour
    {
        public delegate void OnEnableEvent();
        public OnEnableEvent onEnableEvent;
        public delegate void OnDisableEvent();
        public OnEnableEvent onDisableEvent;

        private void OnEnable()
        {
            onEnableEvent?.Invoke();
        }

        private void OnDisable()
        {
            onDisableEvent?.Invoke();
        }
    }
}