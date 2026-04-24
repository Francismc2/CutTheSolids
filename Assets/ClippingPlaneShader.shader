Shader "Custom/ClippingPlane"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _PlanePosition ("Plane Position", Vector) = (0,0,0,0)
        _PlaneNormal ("Plane Normal", Vector) = (0,1,0,0)
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 worldPos : TEXCOORD0;
            };

            float4 _Color;
            float3 _PlanePosition;
            float3 _PlaneNormal;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float d = dot(i.worldPos - _PlanePosition, _PlaneNormal);

                // If behind plane → discard pixel
                if (d < 0)
                    discard;

                return _Color;
            }
            ENDCG
        }
    }
}