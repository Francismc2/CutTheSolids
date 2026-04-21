Shader "Custom/ScrollingAndRotatingOverlayShader"
{
    Properties
    {
        _BaseColor ("Base Color (Tint)", Color) = (1, 1, 1, 1)            // Tint for the base/background texture
        _MainTex ("Main Texture", 2D) = "white" {}                        // Background texture
        _OverlayTex ("Overlay Texture (Scrolling Pattern)", 2D) = "white" {} // Overlay texture
        _OverlayTint ("Overlay Tint (Color)", Color) = (1, 1, 1, 1)       // Tint color for overlay
        _ScrollSpeed ("Scroll Speed", Float) = 0.5                        // Scrolling speed of the pattern
        _ScrollDirection ("Scroll Direction (Degrees)", Float) = 0.0      // Direction of scrolling in degrees
        _Metallic ("Metallic", Range(0, 1)) = 0                           // Material metallic property
        _Smoothness ("Smoothness", Range(0, 1)) = 0.5                     // Material smoothness property
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Geometry" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows

        sampler2D _MainTex;                // Background texture
        sampler2D _OverlayTex;             // Scrolling overlay texture
        float4 _BaseColor;                 // Color tint for base texture
        float4 _OverlayTint;               // Color tint for the overlay
        float _ScrollSpeed;                // Speed of scrolling overlay
        float _ScrollDirection;            // Direction of scrolling (degrees)
        half _Metallic;                    // Metallic value
        half _Smoothness;                  // Smoothness value

        // A constant to convert degrees to radians in HLSL
        static const float DEG_TO_RAD = 0.01745329252; // (PI / 180)

        struct Input
        {
            float2 uv_MainTex;             // UVs for the base texture
            float2 uv_OverlayTex;          // UVs for the overlay texture
        };

        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            // #### Main Texture (Background) ####
            fixed4 baseTex = tex2D(_MainTex, IN.uv_MainTex); // Sample base/background texture
            baseTex *= _BaseColor;                          // Apply base tint
            baseTex.rgb /= (baseTex.a + 0.0001);            // Normalize w.r.t alpha (avoid divide-by-zero)

            // #### Overlay Texture Handling ####

            // Step 1: Scroll overlay in chosen direction
            // Convert scroll direction from degrees to radians
            float radians = _ScrollDirection * DEG_TO_RAD;      // Convert scroll direction (degrees -> radians)
            float2 scrollDir = float2(cos(radians), sin(radians)); // Direction (as unit vector)
            float2 scrolledUV = IN.uv_OverlayTex + scrollDir * (_Time.y * _ScrollSpeed);

            // Step 2: Rotate overlay around its center
            // Center UVs for rotation
            float2 center = float2(0.5, 0.5);                   // Center for the UV space
            float2 uv = scrolledUV - center;                    // Move UV to origin (rotation pivot)
            // Apply the rotation matrix
            float2x2 rotationMatrix = float2x2(
                cos(radians), -sin(radians),
                sin(radians),  cos(radians)
            );
            uv = mul(rotationMatrix, uv);                       // Rotate UVs
            uv += center;                                       // Move UV back to original space

            // Step 3: Sample the overlay texture and apply tint
            fixed4 overlayTex = tex2D(_OverlayTex, uv);         // Sample overlay texture using rotated and scrolled UVs
            overlayTex *= _OverlayTint;                         // Apply overlay tint color

            // #### Combine Overlay with Base Texture ####
            // Blend the overlay with the base texture based on overlay alpha
            o.Albedo = overlayTex.a * overlayTex.rgb + (1 - overlayTex.a) * baseTex.rgb;

            // Apply metallic and smoothness properties
            o.Metallic = _Metallic;
            o.Smoothness = _Smoothness;
        }
        ENDCG
    }

    FallBack "Standard"
}