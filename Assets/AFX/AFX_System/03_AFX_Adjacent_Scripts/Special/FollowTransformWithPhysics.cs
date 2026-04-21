using UnityEngine;
namespace Engage.AFX.v1
{
    [AddComponentMenu("AFX/Special/Follow Transform With Physics")]
    public class FollowTransformWithPhysics : MonoBehaviour
    {
        [SerializeField] bool position = true;
        [SerializeField] bool rotation = true;
        [SerializeField] Transform transformToFollow;
        [SerializeField] Rigidbody rigidbodyToMove;       

        void FixedUpdate()
        {
            if (position)
            {
                rigidbodyToMove.MovePosition((rigidbodyToMove.position + transformToFollow.position) / 2);
            }

            if (rotation)
            {
                rigidbodyToMove.MoveRotation(Quaternion.Lerp(rigidbodyToMove.rotation, transformToFollow.rotation, 0.5f));
            }
        }
    }
}
