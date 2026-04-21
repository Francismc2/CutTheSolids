using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.ComponentRenderingLight + "Light Info")]
    public class LightInfo : AFXNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never)] private Light lightIn;

        [SerializeField]
        [Output] private float rangeOut;
        [SerializeField]
        [Output] private Color colorOut;
        [SerializeField]
        [Output] private float intensityOut;
        [SerializeField]
        [Output] private GameObject gameObject;

        public override object GetValue(NodePort port)
        {
            lightIn = GetInputValue(nameof(lightIn), lightIn);
            if (port.fieldName == nameof(rangeOut))
            {
                rangeOut = lightIn.range;
                return rangeOut;
            }
            if (port.fieldName == nameof(colorOut))
            {
                colorOut = lightIn.color;
                return colorOut;
            }
            if (port.fieldName == nameof(intensityOut))
            {
                intensityOut = lightIn.intensity;
                return colorOut;
            }
            if (port.fieldName == nameof(gameObject))
            {
                gameObject = lightIn.gameObject;
                return colorOut;
            }
            return null;
        }
    }
}