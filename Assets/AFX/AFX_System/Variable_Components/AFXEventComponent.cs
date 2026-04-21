using UnityEngine;
using UnityEngine.Events;
namespace Engage.AFX.v1
{
    [AddComponentMenu("AFX/AFXEvent Component")]
    public class AFXEventComponent : MonoBehaviour
    {
        public AFXEvent afxEvent;
        public UnityEvent onEventTriggered;

        void OnEnable()
        {
            afxEvent.AddListener(this);
        }

        void OnDisable()
        {
            afxEvent.RemoveListener(this);
        }

        public void OnEventTriggered()
        {
            onEventTriggered.Invoke();
        }
    }
}