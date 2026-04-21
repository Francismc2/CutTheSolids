using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.RefVariable + "String Component Reference")]
    public class StringReference : ObjectReferenceNode<StringComponent>
    {
        [SerializeField][Output(ShowBackingValue.Never)] protected string valueOut; // Don't Rename this!

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