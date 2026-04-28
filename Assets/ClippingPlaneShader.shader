Shader "Custom/ClippingPlaneWithCap"
{
    Properties
    {
        _Color ("Outside Color", Color) = (1,1,1,1)
        _CapColor ("Cap Color", Color) = (0.8,0.2,0.2,1)
        _PlanePosition ("Plane Position", Vector) = (0,0,0,0)
        _PlaneNormal ("Plane Normal", Vector) = (0,1,0,0)
        _Shininess ("Shininess", Range(1,128)) = 32
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        // ═══════════════════════════════════════════════
        // FORWARDBASE — first directional light + ambient
        // ═══════════════════════════════════════════════

        // PASS 1 — Front faces (ForwardBase)
        Pass
        {
            Tags { "LightMode"="ForwardBase" }
            Cull Back
            ZWrite On

            Stencil { Ref 0  Comp Always  Pass Zero }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fwdbase
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "AutoLight.cginc"

            struct appdata { float4 vertex : POSITION; float3 normal : NORMAL; };
            struct v2f
            {
                float4 pos       : SV_POSITION;
                float3 worldPos  : TEXCOORD0;
                float3 worldNormal : TEXCOORD1;
                SHADOW_COORDS(2)
            };

            float4 _Color;
            float3 _PlanePosition, _PlaneNormal;
            float  _Shininess;

            v2f vert(appdata v)
            {
                v2f o;
                o.pos        = UnityObjectToClipPos(v.vertex);
                o.worldPos   = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                TRANSFER_SHADOW(o);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                if (dot(i.worldPos - _PlanePosition, normalize(_PlaneNormal)) < 0) discard;

                float3 N = normalize(i.worldNormal);
                float3 L = normalize(_WorldSpaceLightPos0.xyz);
                float3 V = normalize(_WorldSpaceCameraPos - i.worldPos);
                float3 H = normalize(L + V);

                float3 ambient  = ShadeSH9(float4(N, 1));
                float3 diffuse  = _LightColor0.rgb * max(0, dot(N, L));
                float3 specular = _LightColor0.rgb * pow(max(0, dot(N, H)), _Shininess);
                float  shadow   = SHADOW_ATTENUATION(i);

                return fixed4(_Color.rgb * (ambient + (diffuse + specular) * shadow), 1);
            }
            ENDCG
        }

        // PASS 2 — Back faces stencil stamp (ForwardBase)
        Pass
        {
            Tags { "LightMode"="ForwardBase" }
            Cull Front
            ZWrite On
            ColorMask 0

            Stencil { Ref 1  Comp Always  Pass Replace }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag_inside
            #include "UnityCG.cginc"

            struct appdata { float4 vertex : POSITION; };
            struct v2f { float4 pos : SV_POSITION; float3 worldPos : TEXCOORD0; };

            float3 _PlanePosition, _PlaneNormal;

            v2f vert(appdata v)
            {
                v2f o;
                o.pos      = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }

            fixed4 frag_inside(v2f i) : SV_Target
            {
                if (dot(i.worldPos - _PlanePosition, normalize(_PlaneNormal)) < 0) discard;
                return fixed4(0,0,0,1);
            }
            ENDCG
        }

        // PASS 3 — Cap (ForwardBase)
        Pass
        {
            Tags { "LightMode"="ForwardBase" }
            Cull Off
            ZWrite On
            ZTest Always

            Stencil { Ref 1  Comp Equal  Pass Zero }

            CGPROGRAM
            #pragma vertex vert_cap
            #pragma fragment frag_cap
            #pragma multi_compile_fwdbase
            #include "UnityCG.cginc"
            #include "Lighting.cginc"

            struct appdata   { float4 vertex : POSITION; };
            struct v2f_cap   { float4 pos : SV_POSITION; };

            float4 _CapColor;
            float3 _PlanePosition, _PlaneNormal;
            float  _Shininess;

            v2f_cap vert_cap(appdata v)
            {
                v2f_cap o;
                float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                float  dist     = dot(worldPos - _PlanePosition, normalize(_PlaneNormal));
                float3 snapped  = worldPos - dist * normalize(_PlaneNormal);
                o.pos = mul(UNITY_MATRIX_VP, float4(snapped, 1.0));
                return o;
            }

            fixed4 frag_cap(v2f_cap i) : SV_Target
            {
                float3 N       = normalize(_PlaneNormal);
                float3 L       = normalize(_WorldSpaceLightPos0.xyz);
                float3 ambient = ShadeSH9(float4(N, 1));
                float3 diffuse = _LightColor0.rgb * max(0, dot(N, L));
                return fixed4(_CapColor.rgb * (ambient + diffuse), 1);
            }
            ENDCG
        }

        // ═══════════════════════════════════════════════
        // FORWARDADD — runs once per extra light
        //              (spotlights, point lights, extra directionals)
        //              Blended additively on top of ForwardBase result
        // ═══════════════════════════════════════════════

        // PASS 4 — Front faces (ForwardAdd)
        Pass
        {
            Tags { "LightMode"="ForwardAdd" }
            Cull Back
            ZWrite Off          // base pass already wrote depth
            Blend One One       // additive blend over base result

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fwdadd_fullshadows
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "AutoLight.cginc"

            struct appdata { float4 vertex : POSITION; float3 normal : NORMAL; };
            struct v2f
            {
                float4 pos        : SV_POSITION;
                float3 worldPos   : TEXCOORD0;
                float3 worldNormal : TEXCOORD1;
                LIGHTING_COORDS(2, 3)   // works for spot + point + directional
            };

            float4 _Color;
            float3 _PlanePosition, _PlaneNormal;
            float  _Shininess;

            v2f vert(appdata v)
            {
                v2f o;
                o.pos         = UnityObjectToClipPos(v.vertex);
                o.worldPos    = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                TRANSFER_VERTEX_TO_FRAGMENT(o);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                if (dot(i.worldPos - _PlanePosition, normalize(_PlaneNormal)) < 0) discard;

                float3 N = normalize(i.worldNormal);

                // For spot/point lights _WorldSpaceLightPos0.w == 1 and xyz is position
                // For directional lights .w == 0 and xyz is direction
                #ifdef USING_DIRECTIONAL_LIGHT
                    float3 L = normalize(_WorldSpaceLightPos0.xyz);
                    float  atten = 1.0;
                #else
                    float3 L = normalize(_WorldSpaceLightPos0.xyz - i.worldPos);
                    float  atten = LIGHT_ATTENUATION(i);  // handles spot cone + falloff
                #endif

                float3 V = normalize(_WorldSpaceCameraPos - i.worldPos);
                float3 H = normalize(L + V);

                float3 diffuse  = _LightColor0.rgb * max(0, dot(N, L));
                float3 specular = _LightColor0.rgb * pow(max(0, dot(N, H)), _Shininess);

                return fixed4(_Color.rgb * (diffuse + specular) * atten, 1);
            }
            ENDCG
        }

        // PASS 5 — Cap (ForwardAdd)
        Pass
        {
            Tags { "LightMode"="ForwardAdd" }
            Cull Off
            ZWrite Off
            ZTest Always
            Blend One One

            Stencil { Ref 1  Comp Equal }

            CGPROGRAM
            #pragma vertex vert_cap
            #pragma fragment frag_cap
            #pragma multi_compile_fwdadd_fullshadows
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "AutoLight.cginc"

            struct appdata  { float4 vertex : POSITION; };
            struct v2f_cap  { float4 pos : SV_POSITION; float3 worldPos : TEXCOORD0; };

            float4 _CapColor;
            float3 _PlanePosition, _PlaneNormal;

            v2f_cap vert_cap(appdata v)
            {
                v2f_cap o;
                float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                float  dist     = dot(worldPos - _PlanePosition, normalize(_PlaneNormal));
                float3 snapped  = worldPos - dist * normalize(_PlaneNormal);
                o.pos      = mul(UNITY_MATRIX_VP, float4(snapped, 1.0));
                o.worldPos = snapped;
                return o;
            }

            fixed4 frag_cap(v2f_cap i) : SV_Target
            {
                float3 N = normalize(_PlaneNormal);

                #ifdef USING_DIRECTIONAL_LIGHT
                    float3 L    = normalize(_WorldSpaceLightPos0.xyz);
                    float  atten = 1.0;
                #else
                    float3 L    = normalize(_WorldSpaceLightPos0.xyz - i.worldPos);
                    float  atten = 1.0; // cap has no LIGHTING_COORDS, approximate
                #endif

                float3 diffuse = _LightColor0.rgb * max(0, dot(N, L));
                return fixed4(_CapColor.rgb * diffuse * atten, 1);
            }
            ENDCG
        }

        UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
    }
}