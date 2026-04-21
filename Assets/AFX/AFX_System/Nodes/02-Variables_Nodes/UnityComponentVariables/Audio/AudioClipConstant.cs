using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [NodeTitle("AudioClip")]
    [CreateNodeMenu(AFXMenuTree.UnityCompAudio + "AudioClip")]
    public class AudioClipConstant : AFXNode
    {
        [SerializeField][Output(ShowBackingValue.Always)] private AudioClip audioClip;

        public override object GetValue(NodePort port)
        {
            return audioClip;
        }
    }
}