using UnityEngine;
using System.Collections.Generic;
using XNode;
namespace Engage.AFX.v1
{
    [NodeTitle("LayerMask GetMask")]
    [CreateNodeMenu(AFXMenuTree.LayerMask + "LayerMask GetMask")]
    public class LayerMaskGetMask : AFXNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never)] private List<string> layerNameList;

        [SerializeField]
        [Output(ShowBackingValue.Never)] private int output;

        public override object GetValue(NodePort port)
        {
            layerNameList = GetInputValue(nameof(layerNameList), layerNameList);
            return LayerMask.GetMask(layerNameList.ToArray());
        }
    }
}