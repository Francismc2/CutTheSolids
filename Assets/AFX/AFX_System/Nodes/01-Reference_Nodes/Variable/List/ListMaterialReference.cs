using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.RefVariableList + "List Material Reference")]
    public class ListMaterialReference : ObjectReferenceNode<ListMaterialComponent>
    {
        [SerializeField]
        [Output(ShowBackingValue.Never)] protected List<Material> valueOut; // Don't Rename this!

        public override object GetValue(NodePort port)
        {
            if (port.fieldName == ObjectOutPortName)
            {
                return ObjectOut;
            }

            if (port.fieldName == nameof(valueOut))
            {
                var component = ObjectOut;
                return component.Value;
            }

            return null;
        }
    }
}