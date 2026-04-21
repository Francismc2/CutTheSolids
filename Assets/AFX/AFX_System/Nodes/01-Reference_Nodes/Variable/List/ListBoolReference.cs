using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.RefVariableList + "List Bool Reference")]
    public class ListBoolReference : ObjectReferenceNode<ListBoolComponent>
    {
        [SerializeField]
        [Output(ShowBackingValue.Never)] protected List<bool> valueOut; // Don't Rename this!

        public override object GetValue(NodePort port)
        {
            if (port.fieldName == ObjectOutPortName)
            {
                return ObjectOut;
            }

            if (port.fieldName == nameof(valueOut))
            {
                return ObjectOut.Value;
            }

            return null;
        }
    }
}