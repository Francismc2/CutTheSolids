using UnityEngine;

namespace Engage.AFX.v1
{
    public static class AFXNodeColors
    {
        public static readonly Color32 Normal = new Color32(55, 58, 64, 255);
        public static readonly Color Active = Normal;
        public static readonly Color Reference = Normal;
        public static readonly Color Error = new Color(0.9f, 0.5f, 0.1f);
        public static readonly Color Subgraph = new Color(0.3f, 0.3f, 0.3f);

        public static readonly Color noodleActiveGradient1 = Color.white;
        public static readonly Color noodleActiveGradient2 = new Color(.5f, 3f, 3f);

        public static readonly Color noodleInactiveGradient = new Color(0.7f, 0.4f, 0.4f);

        public const string TransformNodesHEX = "#7DDA58";

        public static string HexOfColor(Color color)
        {
            return ColorUtility.ToHtmlStringRGB(color);
        }
    }
}