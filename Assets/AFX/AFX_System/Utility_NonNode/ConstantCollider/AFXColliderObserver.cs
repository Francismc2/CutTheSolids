// /*-------------------------------------------
// ---------------------------------------------
// Creation Date: 29/10/21
// Author: Bryn
// Description: Engage
// Immersive VR Education
// ---------------------------------------------
// -------------------------------------------*/
using UnityEngine;
using System.Collections.Generic;
namespace Engage.AFX.v1
{
    public class AFXColliderObserver : MonoBehaviour
    {
        public List<Collider> currentlyCollidingWith = new List<Collider>();
        private Dictionary<Collider, AFXColliderInformant> informants = new Dictionary<Collider, AFXColliderInformant>();

        public Collision OnCollisionEnterCollision;
        public Collision OnCollisionStayCollision;
        public Collision OnCollisionExitCollision;
        public Collider OnTriggerEnterCollider;
        public Collider OnTriggerStayCollider;
        public Collider OnTriggerExitCollider;

        public void AddInformant(Collider collider)
        {
            if (!collider)
            {
                Debug.LogError($"[{this}.AddInformant] Collider is null.");
                return;
            }

            if (informants.ContainsKey(collider))
            {
                Debug.LogWarning($"[{this}.AddInformant] {collider.name} is already and informant.");
                return;
            }
            var informant = collider.gameObject.AddComponent<AFXColliderInformant>();
            informant.SetObserver(this);
            informants.Add(collider, informant);
        }

        public bool RemoveInformant(Collider collider)
        {
            if (!collider)
            {
                Debug.LogError($"[{this}.AddInformant] Collider is null.");
                return false;
            }

            if (informants.ContainsKey(collider))
            {
                Destroy(informants[collider]);
                return informants.Remove(collider);
            }
            return false;
        }

        public void CollisionEnter(AFXColliderInformant informant, Collision collision)
        {
            currentlyCollidingWith.Add(collision.collider);
        }
        //public void CollisionStay(ColliderInformant informant, Collision collision)
        //{
        //    currentlyCollidingWith[1] = collision.collider;
        //}
        public void CollisionExit(AFXColliderInformant informant, Collision collision)
        {
            currentlyCollidingWith.Remove(collision.collider);
        }
        public void TriggerEnter(AFXColliderInformant informant, Collider collider)
        {
            currentlyCollidingWith.Add(collider);
        }
        //public void TriggerStay(ColliderInformant informant, Collider collider)
        //{
        //    currentlyCollidingWith[0] = collider;
        //}
        public void TriggerExit(AFXColliderInformant informant, Collider collider)
        {
            currentlyCollidingWith.Remove(collider);
        }

        private void OnDestroy()
        {
            foreach (var collider in informants.Keys)
            {
                RemoveInformant(collider);
            }
        }
    }
}