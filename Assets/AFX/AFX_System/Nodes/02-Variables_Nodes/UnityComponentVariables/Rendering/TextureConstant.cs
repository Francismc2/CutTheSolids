using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [NodeTitle("Texture")]
    [CreateNodeMenu(AFXMenuTree.UnityCompRendering + "Texture")]
    public class TextureConstant : AFXNode
    {
        [SerializeField][Output(ShowBackingValue.Always)] private Texture texture;

        public override object GetValue(NodePort port)
        {
            return texture;
        }
    }
}