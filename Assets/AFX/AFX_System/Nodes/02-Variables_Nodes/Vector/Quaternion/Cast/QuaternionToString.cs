using System;
using UnityEngine;
using XNode;

namespace Engage.AFX.v1.Variable.QuaternionNodes.v1
{
    [CreateNodeMenu(AFXMenuTree.QuaternionCast + "Quaternion to String")]
    public class QuaternionToString : AFXNode
    {
        [SerializeField] [Input(ShowBackingValue.Never)] private Quaternion quaternion;

        [SerializeField] [Output(ShowBackingValue.Never)] private String output;

        public override object GetValue(NodePort port)
        {
            quaternion = GetInputValue(nameof(quaternion), quaternion);
            output = quaternion.ToString();

            return output;
        }
    }
}