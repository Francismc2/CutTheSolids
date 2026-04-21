Shader "Custom/DistanceLerpShaderWithFloats"
{
    Properties
    {
        _Color1 ("Color 1", Color) = (1, 0, 0, 1) // Start color
        _Color2 ("Color 2", Color) = (0, 0, 1, 1) // End color
        _TargetX ("Target X Position", Float) = 0.0 // X coordinate of target position
        _TargetY ("Target Y Position", Float) = 0.0 // Y coordinate of target position
        _TargetZ ("Target Z Position", Float) = 0.0 // Z coordinate of target position
        _MaxDistance ("Max Distance", Float) = 10.0 // Maximum distance for interpolation
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 worldPos : TEXCOORD0;
            };

            // Shader properties
            float _TargetX; // X coordinate of the target position
            float _TargetY; // Y coordinate of the target position
            float _TargetZ; // Z coordinate of the target position
            float4 _Color1; // Color 1
            float4 _Color2; // Color 2
            float _MaxDistance; // Max distance for interpolation

            v2f vert (appdata v)
            {
                v2f o;
                // Transform the vertex to clip space
                o.pos = UnityObjectToClipPos(v.vertex);
                // Calculate the world position for distance calculation
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Construct target position from the individual floats
                float3 targetPosition = float3(_TargetX, _TargetY, _TargetZ);

                // Calculate distance between the fragment position and the target position
                float distToPoint = distance(i.worldPos, targetPosition);

                // Clamp the distance to the range [0, _MaxDistance]
                float t = saturate(distToPoint / _MaxDistance);

                // Lerp between the two colors based on the normalized distance factor
                fixed4 result = lerp(_Color1, _Color2, t);
                return result;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}