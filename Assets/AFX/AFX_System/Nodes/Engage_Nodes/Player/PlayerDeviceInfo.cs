using UnityEngine;
using XNode;

namespace Engage.AFX.v1.Player.v1
{
    [CreateNodeMenu(AFXMenuTree.Player + "Player Device Info")]
    public class PlayerDeviceInfo : AFXNode, IDelayUntilReady
    {
        [SerializeField] [Output] private bool isVROut;
        [SerializeField] [Output] private bool IsAndroidOut;
        [SerializeField] [Output] private bool IsDesktopOut;
        [SerializeField] [Output] private bool IsHeadset6dofOut;
        [SerializeField] [Output] private bool IsIOSOut;
        [SerializeField] [Output] private bool IsMacOut;
        [SerializeField] [Output] private bool IsPhoneOut;
        [SerializeField] [Output] private bool IsWindowsOut;
        [SerializeField] [Output] private string DeviceNameOut;


        public override object GetValue(NodePort port)
        {
            return null;
        }

        bool IDelayUntilReady.IsValueReady()
        {
            return true;
        }
    }
}