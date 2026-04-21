using System;
using System.Collections.Generic;
using UnityEngine;
namespace Engage.AFX.v1
{
    [Serializable]
    public class AFXColliders
    {   
        public bool Colliding
        {
            get 
            {
                if (mainCollider == null || targetColliders == null || gameObjectToUse == null)
                {
                    return false;
                }

                switch (collisionMode)
                {
                    case CollisionDetectionType.BoundingBox:
                        return GetCollisionBounds(targetColliders, mainCollider);

                    case CollisionDetectionType.Approxomite:
                        foreach (var collider in targetColliders)
                        {
                            //Debug.Log("aprox");

                            return ColliderContainsCollider(collider, mainCollider);
                        }
                        return false;

                    case CollisionDetectionType.Full:
                        if (colliderObserver == null)
                        {
                            //Debug.Log("created observer");
                            colliderObserver = gameObjectToUse.AddComponent<AFXColliderObserver>();
                            colliderObserver.AddInformant(mainCollider);
                        }
                        return GetCollisionAdvanced(targetColliders, colliderObserver);
                    default:
                        return false;
                }               
                
            }
        }
        public enum CollisionDetectionType
        {
            BoundingBox,
            Approxomite,
            Full
        }

        [SerializeField]
        CollisionDetectionType collisionMode;
        [SerializeField]
        public List<Collider> targetColliders;
        [SerializeField]
        public Collider mainCollider;
        AFXColliderObserver colliderObserver;
        GameObject gameObjectToUse;

        public AFXColliders(GameObject gameObjectIN)
        {
            Init(gameObjectIN);            
        }

        public void Init(GameObject gameObjectIN)
        {
            gameObjectToUse = gameObjectIN;
        }        

        public static Vector3 GetCollidersCentersByType(Collider colliderIn)
        {
            if (colliderIn is BoxCollider)
            {
                return ((BoxCollider)colliderIn).center;
            }
            else if (colliderIn is SphereCollider)
            {
                return ((SphereCollider)colliderIn).center;
            }

            else if (colliderIn is CapsuleCollider)
            {
                return ((CapsuleCollider)colliderIn).center;
            }
            else if (colliderIn is MeshCollider)
            {
                return ((MeshCollider)colliderIn).transform.localPosition;
            }
            return Vector3.zero;
        }

        public static bool ColliderContainsPoint(Collider collider, Vector3 targetsCenter)
        {
            if (collider.bounds.Contains(targetsCenter)) // check to see if a more basic form of collision is true before doing the more complex version
            {
                Vector3 collidercenterworld = collider.transform.TransformPoint(GetCollidersCentersByType(collider));
                Vector3 colliderclosestPointtarget;
                colliderclosestPointtarget = collider.ClosestPoint(targetsCenter);
                float distanceToSelf = (collidercenterworld - colliderclosestPointtarget).sqrMagnitude;
                float distanceToTarget = (collidercenterworld - targetsCenter).sqrMagnitude;               
                if (distanceToTarget <= distanceToSelf)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool ColliderContainsCollider(Collider collider, Collider targetCollider)
        {
            if (collider.bounds.Intersects(targetCollider.bounds)) // check to see if a more basic form of collision is true before doing the more complex version
            {
                Vector3 collidercenterworld = collider.transform.TransformPoint(GetCollidersCentersByType(collider));              
                Vector3 targetColliderclosestPointtarget = targetCollider.ClosestPoint(collidercenterworld);
                Vector3 colliderclosestPointtarget = collider.ClosestPoint(targetColliderclosestPointtarget);
                float distanceToSelf = (collidercenterworld - colliderclosestPointtarget).sqrMagnitude;
                float distanceToTargetsClosestPoint = (collidercenterworld - targetColliderclosestPointtarget).sqrMagnitude;
                if (distanceToTargetsClosestPoint <= distanceToSelf)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool GetCollisionAdvanced(List<Collider> targetColliders, AFXColliderObserver colliderObserver)
        {
            foreach (var target in targetColliders)
            {
                foreach (var collider in colliderObserver.currentlyCollidingWith)
                {
                    if (collider == target)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private static bool GetCollisionBounds(List<Collider> targetColliders, Collider mainCollider)
        {
            foreach (var target in targetColliders)
            {
                if (mainCollider.bounds.Intersects(target.bounds))
                {
                    return true;
                }
            }
            return false;
        }
    }
}