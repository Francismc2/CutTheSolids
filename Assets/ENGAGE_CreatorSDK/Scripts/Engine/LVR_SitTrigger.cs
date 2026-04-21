using UnityEngine;
using System.Collections;
//using Engage.Avatars.Poses;

public class LVR_SitTrigger : MonoBehaviour
{
    public Transform floorCollider;
    float lowerHeadAmount = 0.6f;

    public Vector3 m_seatPosition = new Vector3(0f, 0.57f, 0.15f);
    Vector3 m_leftFootPos = new Vector3(-0.1f, 0.11f, 0.23f);
    Vector3 m_rightFootPos = new Vector3(0.1f, 0.11f, 0.23f);
    [Header("Special use cases - Rarely used - Be Careful")]
    public bool lockedInSeat = false;
    [SerializeField] private bool movingEffect = false;



    private Transform _pelvisTarget;

    Transform PelvisTarget
    {
        get
        {
            if (_pelvisTarget == null)
            {
                _pelvisTarget = new GameObject("Root").transform;
                _pelvisTarget.SetParent(floorCollider.transform); _pelvisTarget.localRotation = Quaternion.identity;
            }
            //Many seats are oddly scaled, so we use transformDirection to ensure real values are used.
            _pelvisTarget.position = floorCollider.position + floorCollider.TransformDirection(m_seatPosition);
            return _pelvisTarget;
        }
    }



#if UNITY_EDITOR

    private Vector3 m_floorPlaneSize = new Vector3(1f, .01f, 1f);
    private Color m_floorColour = new Color(0f, 1f, 0f, .5f);


    private void OnDrawGizmos()
    {
        if (floorCollider != null)
        {
            Gizmos.color = m_floorColour;
            Gizmos.DrawCube(floorCollider.position, m_floorPlaneSize);
            Gizmos.color = Color.white;
        }
        //if (GetComponent<PoseSelector>() == null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(floorCollider.transform.TransformPoint(m_seatPosition) - floorCollider.up * 0.0525f, new Vector3(0.1f, 0.01f, 0.1f));
        }
        DrawSphere(floorCollider.transform.TransformPoint(m_seatPosition), Color.red, 0.06f);
        DrawArrow(floorCollider.transform.TransformPoint(m_seatPosition), floorCollider.transform.forward * 0.5f, Color.red);
    }
    private void DrawSphere(Vector3 pos, Color colour, float size = .06f)
    {
        Gizmos.color = colour;

        Gizmos.DrawSphere(pos, size);

        Gizmos.color = Color.white;
    }

    private void DrawArrow(Vector3 pos, Vector3 direction, Color color, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f)
    {
        Gizmos.color = color;
        Gizmos.DrawRay(pos, direction);

        Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 + arrowHeadAngle, 0) * new Vector3(0, 0, 1);
        Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 - arrowHeadAngle, 0) * new Vector3(0, 0, 1);
        Gizmos.DrawRay(pos + direction, right * arrowHeadLength);
        Gizmos.DrawRay(pos + direction, left * arrowHeadLength);
    }

#endif
}
