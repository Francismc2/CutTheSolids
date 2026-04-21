using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Engage.IFX.Options
{
    public class BrandedIFXEffect : MonoBehaviour
    {
        [Header("")]
        [Header("List Renderers for Branded IFX Images")]
        [Header("")]
        [Header("This script should be on the root of IFX object")]
        public List<BrandedIFXRenderer> brandedRenderers = new List<BrandedIFXRenderer>();

        [System.Serializable]
        public enum BrandedTextureFileType
        {
            Opaque_1024x1024 = 2,
            Opaque_1024x512 = 3,
            Opaque_512x1024 = 4,
            Transparent_Light_1024x1024 = 5,
            Transparent_Light_1024x512 = 6,
            Transparent_Light_512x1024 = 7,
            Transparent_Dark_1024x1024 = 8,
            Transparent_Dark_1024x512 = 9,
            Transparent_Dark_512x1024 = 10
        }

        [System.Serializable]
        public class BrandedIFXRenderer
        {
            public BrandedTextureFileType file_type;
            [Header("The renderer to add the branded logo")]
            public Renderer _renderer;
            [Header("(Shared) Material Index - Normally 0 unless multiple materials are on the renderer")]
            public int MaterialIndex = 0;
            [Header("Texture Key of shader to apply branding - Default is _MainTex")]
            public string _textureKey = "_MainTex";
        }
    }
}
