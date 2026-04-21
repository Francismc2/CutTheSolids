using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.RefVariableList + "List Float Reference")]
    public class ListFloatReference : ObjectReferenceNode<ListFloatComponent>
    {
        [SerializeField]
        [Output(ShowBackingValue.Never)] protected List<float> valueOut; // Don't Rename this!

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