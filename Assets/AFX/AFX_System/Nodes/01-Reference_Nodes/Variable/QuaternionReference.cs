using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.RefVariable + "Quaternion Component Reference")]
    public class QuaternionReference : ObjectReferenceNode<QuaternionComponent>
    {
        [SerializeField][Output(ShowBackingValue.Never)] protected Quaternion valueOut; // Don't Rename this!

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