using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.RefVariableList + "List PathData Reference")]
    public class ListPathDataReference : ObjectReferenceNode<ListPathDataComponent>
    {
        [SerializeField]
        [Output(ShowBackingValue.Never)] protected List<PathData> valueOut; // Don't Rename this!

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