using System;
using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.StringCast + "Cast String to Float")]
    public class StringToFloat : AFXNode
    {
        [SerializeField][Input(ShowBackingValue.Unconnected)] private String input;

        [SerializeField][Output] private float output;

        public override object GetValue(NodePort port)
        {
            input = GetInputValue(nameof(input), input);

            if(!float.TryParse(input, out output))
            {
                error = $"[{Graph.name}] [{GetType()}] String {input} not parsable as float.";
                Debug.LogError(error);
            }

            return output;
        }
    }
}