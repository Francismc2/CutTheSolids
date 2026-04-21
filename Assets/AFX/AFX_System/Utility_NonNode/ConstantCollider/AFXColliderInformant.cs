// /*-------------------------------------------
// ---------------------------------------------
// Creation Date: 29/10/21
// Author: Bryn
// Description: Engage
// Immersive VR Education
// ---------------------------------------------
// -------------------------------------------*/
using UnityEngine;
namespace Engage.AFX.v1
{
    [RequireComponent(typeof(Collider))]
    public class AFXColliderInformant : MonoBehaviour
    {
        private AFXColliderObserver observer;

        public void SetObserver(AFXColliderObserver observer)
        {
            this.observer = observer;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (observer)
            {
                observer.CollisionEnter(this, collision);
            }
        }

        //private void OnCollisionStay(Collision collision)
        //{
        //    if (observer)
        //    {
        //        observer.CollisionStay(this, collision);
        //    }
        //}

        private void OnCollisionExit(Collision collision)
        {
            if (observer)
            {
                observer.CollisionExit(this, collision);
            }
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (observer)
            {
                observer.TriggerEnter(this, collider);
            }
        }

        //private void OnTriggerStay(Collider collider)
        //{
        //    if (observer)
        //    {
        //        observer.TriggerStay(this, collider);
        //    }
        //}

        private void OnTriggerExit(Collider collider)
        {
            if (observer)
            {
                observer.TriggerExit(this, collider);
            }
        }
    }
}