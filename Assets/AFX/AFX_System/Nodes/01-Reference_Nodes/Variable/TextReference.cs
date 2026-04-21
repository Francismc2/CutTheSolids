using UnityEngine;
using UnityEngine.UI;
using XNode;

namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.RefVariable + "Text Component Reference")]
    public class TextReference : ObjectReferenceNode<Text>
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
                valueOut = ObjectOut.text;
                return valueOut;
            }

            return null;
        }
    }
}