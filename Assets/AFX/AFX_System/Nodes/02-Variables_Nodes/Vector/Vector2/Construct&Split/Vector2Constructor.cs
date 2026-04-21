using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [NodeTitle("Build Vector2")]
    [CreateNodeMenu(AFXMenuTree.Vector2 + "Build Vector2")]
    public class Vector2Constructor : AFXNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float xIn;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float yIn;

        [SerializeField]
        [Output] private Vector2 output;

        public override object GetValue(NodePort port)
        {
            output = new Vector2(GetInputValue(nameof(xIn), xIn), GetInputValue(nameof(yIn), yIn));
            return output;
        }
    }
}