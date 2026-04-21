using UnityEngine;
namespace Engage.AFX.v1
{
    [RequireComponent(typeof(Collider))]
    public class AFXColliderEventPassthrough : MonoBehaviour
    {
        public delegate Collision OnCollisionEnterEvent(Collision collision);
        public OnCollisionEnterEvent onCollisionEnterEvent;

        public delegate Collision OnCollisionStayEvent(Collision collision);
        public OnCollisionStayEvent onCollisionStayEvent;

        public delegate Collision OnCollisionExitEvent(Collision collision);
        public OnCollisionExitEvent onCollisionExitEvent;

        public delegate Collider OnTriggerEnterEvent(Collider collider);
        public OnTriggerEnterEvent onTriggerEnterEvent;

        public delegate Collider OnTriggerStayEvent(Collider collider);
        public OnTriggerStayEvent onTriggerStayEvent;

        public delegate Collider OnTriggerExitEvent(Collider collider);
        public OnTriggerExitEvent onTriggerExitEvent;

        private void OnCollisionEnter(Collision collision)
        {
            onCollisionEnterEvent?.Invoke(collision);
        }

        private void OnCollisionStay(Collision collision)
        {
            onCollisionStayEvent?.Invoke(collision);
        }

        private void OnCollisionExit(Collision collision)
        {
            onCollisionExitEvent?.Invoke(collision);
        }

        private void OnTriggerEnter(Collider collider)
        {
            onTriggerEnterEvent?.Invoke(collider);
        }

        private void OnTriggerStay(Collider collider)
        {

            onTriggerStayEvent?.Invoke(collider);
        }

        private void OnTriggerExit(Collider collider)
        {
            onTriggerExitEvent?.Invoke(collider);
        }
    }
}