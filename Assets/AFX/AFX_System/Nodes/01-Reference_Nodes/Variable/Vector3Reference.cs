using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.RefVariable + "Vector3 Component Reference")]
    public class Vector3Reference : ObjectReferenceNode<Vector3Component>
    {
        [SerializeField][Output(ShowBackingValue.Never)] protected Vector3 valueOut; // Don't Rename this!

        public override object GetValue(NodePort port)
        {
            if (port.fieldName == ObjectOutPortName)
            {
                return ObjectOut;
            }

            if (port.fieldName == nameof(valueOut))
            {
                if (ObjectOut == null) return valueOut;
                valueOut = ObjectOut.Value;
                return valueOut;
            }

            return null;
        }
    }
}