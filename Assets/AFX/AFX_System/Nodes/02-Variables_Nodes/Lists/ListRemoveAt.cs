using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace Engage.AFX.v1
{
    [NodeTitle("RemoveAt")]
    [CreateNodeMenu(AFXMenuTree.List + "List Remove at Index")]
    public class ListRemoveAt : AFXActiveNode
    {
        private const string PortNameIn1 = "List";

        [SerializeField][Input(ShowBackingValue.Unconnected)] private int index;

        protected override void Init()
        {
            this.InitPort<List<object>>(PortNameIn1);
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            var list = (IList)GetInputPort(PortNameIn1).GetInputValue();
            index = GetInputValue(nameof(index), index);
            if (index < 0 || index > list.Count - 1)
            {
                Error = $"Index out of range: [{index}]";
                return;
            }

            list.RemoveAt(index);

            base.ExecuteNode(exit);
        }
    }
}