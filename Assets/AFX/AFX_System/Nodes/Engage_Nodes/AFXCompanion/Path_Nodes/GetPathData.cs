using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [NodeTitle("Get PathData")]
    [CreateNodeMenu(AFXMenuTree.PathData + "Get PathData")]
    public class GetPathData : AFXNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never)] private PathData pathDataIn;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float timeIn;

        [SerializeField]
        [Output] private Vector3 vector3Out;
        [SerializeField]
        [Output] private int keyCount;
        [SerializeField]
        [Output] private int keyIndexAtCurrentTime;

        public override object GetValue(NodePort port)
        {
            pathDataIn = GetInputValue(nameof(pathDataIn), pathDataIn);
            timeIn = GetInputValue(nameof(timeIn), timeIn);

            if (pathDataIn == null )
            {
                return null;
            }
            if (port.fieldName == nameof(vector3Out))
            {
                return pathDataIn.path.GetPositionOnPath(timeIn);
            }
            if (port.fieldName == nameof(keyCount))
            {
                return pathDataIn.path.KeyCount;
            }
            if (port.fieldName == nameof(keyIndexAtCurrentTime))
            {
                return pathDataIn.path.GetClosestKeyToTime(timeIn, pathDataIn.path.pathX);
            }
            return null;
        }
    }
}