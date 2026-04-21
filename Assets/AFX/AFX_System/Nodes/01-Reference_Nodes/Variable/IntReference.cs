using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.RefVariable + "Int Component Reference")]
    public class IntReference : ObjectReferenceNode<IntComponent>
    {
        [SerializeField] [Output(ShowBackingValue.Never)] protected int valueOut; // Don't Rename this!

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