Shader "Custom/ClippingPlaneWithCap"
{
    Properties
    {
        _Color ("Outside Color", Color) = (1,1,1,1)
        _CapColor ("Cap Color", Color) = (0.8,0.2,0.2,1)

        _PlanePosX ("Plane Position X", Float) = 0
        _PlanePosY ("Plane Position Y", Float) = 0
        _PlanePosZ ("Plane Position Z", Float) = 0

        _PlaneNormX ("Plane Normal X", Float) = 0
        _PlaneNormY ("Plane Normal Y", Float) = 1
        _PlaneNormZ ("Plane Normal Z", Float) = 0

        _Shininess ("Shininess", Range(1,128)) = 32
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        // =========================================================
        // FORWARD BASE
        // =========================================================

        // ---------------------------------------------------------
        // PASS 1 — FRONT FACES
        // ---------------------------------------------------------
        Pass
        {
            Tags { "LightMode"="ForwardBase" }

            Cull Back
            ZWrite On

            Stencil
            {
                Ref 0
                Comp Always
                Pass Zero
            }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fwdbase

            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "AutoLight.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos         : SV_POSITION;
                float3 worldPos    : TEXCOORD0;
                float3 worldNormal : TEXCOORD1;

                SHADOW_COORDS(2)
            };

            float4 _Color;

            float _PlanePosX;
            float _PlanePosY;
            float _PlanePosZ;

            float _PlaneNormX;
            float _PlaneNormY;
            float _PlaneNormZ;

            float _Shininess;

            v2f vert(appdata v)
            {
                v2f o;

                o.pos = UnityObjectToClipPos(v.vertex);

                o.worldPos =
                    mul(unity_ObjectToWorld, v.vertex).xyz;

                o.worldNormal =
                    UnityObjectToWorldNormal(v.normal);

                TRANSFER_SHADOW(o);

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float3 planePos =
                    float3(_PlanePosX, _PlanePosY, _PlanePosZ);

                float3 planeNormal =
                    normalize(float3(
                        _PlaneNormX,
                        _PlaneNormY,
                        _PlaneNormZ));

                // CLIP
                if (dot(i.worldPos - planePos, planeNormal) < 0)
                    discard;

                float3 N = normalize(i.worldNormal);
                float3 L = normalize(_WorldSpaceLightPos0.xyz);
                float3 V = normalize(_WorldSpaceCameraPos - i.worldPos);
                float3 H = normalize(L + V);

                float3 ambient =
                    ShadeSH9(float4(N, 1));

                float3 diffuse =
                    _LightColor0.rgb * max(0, dot(N, L));

                float3 specular =
                    _LightColor0.rgb *
                    pow(max(0, dot(N, H)), _Shininess);

                float shadow =
                    SHADOW_ATTENUATION(i);

                float3 finalColor =
                    _Color.rgb *
                    (ambient + (diffuse + specular) * shadow);

                return fixed4(finalColor, 1);
            }

            ENDCG
        }

        // ---------------------------------------------------------
        // PASS 2 — BACK FACE STENCIL
        // ---------------------------------------------------------
        Pass
        {
            Tags { "LightMode"="ForwardBase" }

            Cull Front
            ZWrite On
            ColorMask 0

            Stencil
            {
                Ref 1
                Comp Always
                Pass Replace
            }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag_inside

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

            float _PlanePosX;
            float _PlanePosY;
            float _PlanePosZ;

            float _PlaneNormX;
            float _PlaneNormY;
            float _PlaneNormZ;

            v2f vert(appdata v)
            {
                v2f o;

                o.pos =
                    UnityObjectToClipPos(v.vertex);

                o.worldPos =
                    mul(unity_ObjectToWorld, v.vertex).xyz;

                return o;
            }

            fixed4 frag_inside(v2f i) : SV_Target
            {
                float3 planePos =
                    float3(_PlanePosX, _PlanePosY, _PlanePosZ);

                float3 planeNormal =
                    normalize(float3(
                        _PlaneNormX,
                        _PlaneNormY,
                        _PlaneNormZ));

                if (dot(i.worldPos - planePos, planeNormal) < 0)
                    discard;

                return fixed4(0,0,0,1);
            }

            ENDCG
        }

        // ---------------------------------------------------------
        // PASS 3 — CAP
        // ---------------------------------------------------------
        Pass
        {
            Tags { "LightMode"="ForwardBase" }

            Cull Off
            ZWrite On
            ZTest LEqual

            // Helps reduce z-fighting
            Offset -1, -1

            Stencil
            {
                Ref 1
                Comp Equal
                Pass Zero
            }

            CGPROGRAM
            #pragma vertex vert_cap
            #pragma fragment frag_cap
            #pragma multi_compile_fwdbase

            #include "UnityCG.cginc"
            #include "Lighting.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f_cap
            {
                float4 pos : SV_POSITION;
                float3 worldPos : TEXCOORD0;
            };

            float4 _CapColor;

            float _PlanePosX;
            float _PlanePosY;
            float _PlanePosZ;

            float _PlaneNormX;
            float _PlaneNormY;
            float _PlaneNormZ;

            v2f_cap vert_cap(appdata v)
            {
                v2f_cap o;

                float3 planePos =
                    float3(_PlanePosX, _PlanePosY, _PlanePosZ);

                float3 planeNormal =
                    normalize(float3(
                        _PlaneNormX,
                        _PlaneNormY,
                        _PlaneNormZ));

                float3 worldPos =
                    mul(unity_ObjectToWorld, v.vertex).xyz;

                float dist =
                    dot(worldPos - planePos, planeNormal);

                // Project vertex onto clipping plane
                float3 snapped =
                    worldPos - dist * planeNormal;

                // IMPORTANT:
                // write correct depth on clipping plane
                o.pos =
                    UnityWorldToClipPos(float4(snapped, 1.0));

                o.worldPos = snapped;

                return o;
            }

            fixed4 frag_cap(v2f_cap i) : SV_Target
            {
                float3 planeNormal =
                    normalize(float3(
                        _PlaneNormX,
                        _PlaneNormY,
                        _PlaneNormZ));

                float3 N = planeNormal;
                float3 L = normalize(_WorldSpaceLightPos0.xyz);

                float3 ambient =
                    ShadeSH9(float4(N, 1));

                float3 diffuse =
                    _LightColor0.rgb *
                    max(0, dot(N, L));

                float3 finalColor =
                    _CapColor.rgb * (ambient + diffuse);

                return fixed4(finalColor, 1);
            }

            ENDCG
        }

        // =========================================================
        // FORWARD ADD
        // =========================================================

        // ---------------------------------------------------------
        // PASS 4 — FRONT FACES ADDITIVE
        // ---------------------------------------------------------
        Pass
        {
            Tags { "LightMode"="ForwardAdd" }

            Cull Back
            ZWrite Off
            Blend One One

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fwdadd_fullshadows

            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "AutoLight.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 worldPos : TEXCOORD0;
                float3 worldNormal : TEXCOORD1;

                LIGHTING_COORDS(2,3)
            };

            float4 _Color;

            float _PlanePosX;
            float _PlanePosY;
            float _PlanePosZ;

            float _PlaneNormX;
            float _PlaneNormY;
            float _PlaneNormZ;

            float _Shininess;

            v2f vert(appdata v)
            {
                v2f o;

                o.pos =
                    UnityObjectToClipPos(v.vertex);

                o.worldPos =
                    mul(unity_ObjectToWorld, v.vertex).xyz;

                o.worldNormal =
                    UnityObjectToWorldNormal(v.normal);

                TRANSFER_VERTEX_TO_FRAGMENT(o);

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float3 planePos =
                    float3(_PlanePosX, _PlanePosY, _PlanePosZ);

                float3 planeNormal =
                    normalize(float3(
                        _PlaneNormX,
                        _PlaneNormY,
                        _PlaneNormZ));

                if (dot(i.worldPos - planePos, planeNormal) < 0)
                    discard;

                float3 N =
                    normalize(i.worldNormal);

                #ifdef USING_DIRECTIONAL_LIGHT

                    float3 L =
                        normalize(_WorldSpaceLightPos0.xyz);

                    float atten = 1.0;

                #else

                    float3 L =
                        normalize(_WorldSpaceLightPos0.xyz - i.worldPos);

                    float atten =
                        LIGHT_ATTENUATION(i);

                #endif

                float3 V =
                    normalize(_WorldSpaceCameraPos - i.worldPos);

                float3 H =
                    normalize(L + V);

                float3 diffuse =
                    _LightColor0.rgb *
                    max(0, dot(N, L));

                float3 specular =
                    _LightColor0.rgb *
                    pow(max(0, dot(N, H)), _Shininess);

                return fixed4(
                    _Color.rgb *
                    (diffuse + specular) *
                    atten,
                    1);
            }

            ENDCG
        }

        // ---------------------------------------------------------
        // PASS 5 — CAP ADDITIVE
        // ---------------------------------------------------------
        Pass
        {
            Tags { "LightMode"="ForwardAdd" }

            Cull Off
            ZWrite Off
            ZTest LEqual

            Blend One One

            Offset -1, -1

            Stencil
            {
                Ref 1
                Comp Equal
                Pass Zero
            }

            CGPROGRAM
            #pragma vertex vert_cap
            #pragma fragment frag_cap
            #pragma multi_compile_fwdadd_fullshadows

            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "AutoLight.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f_cap
            {
                float4 pos : SV_POSITION;
                float3 worldPos : TEXCOORD0;
            };

            float4 _CapColor;

            float _PlanePosX;
            float _PlanePosY;
            float _PlanePosZ;

            float _PlaneNormX;
            float _PlaneNormY;
            float _PlaneNormZ;

            v2f_cap vert_cap(appdata v)
            {
                v2f_cap o;

                float3 planePos =
                    float3(_PlanePosX, _PlanePosY, _PlanePosZ);

                float3 planeNormal =
                    normalize(float3(
                        _PlaneNormX,
                        _PlaneNormY,
                        _PlaneNormZ));

                float3 worldPos =
                    mul(unity_ObjectToWorld, v.vertex).xyz;

                float dist =
                    dot(worldPos - planePos, planeNormal);

                float3 snapped =
                    worldPos - dist * planeNormal;

                // IMPORTANT:
                // correct depth
                o.pos =
                    UnityWorldToClipPos(float4(snapped, 1.0));

                o.worldPos = snapped;

                return o;
            }

            fixed4 frag_cap(v2f_cap i) : SV_Target
            {
                float3 planeNormal =
                    normalize(float3(
                        _PlaneNormX,
                        _PlaneNormY,
                        _PlaneNormZ));

                float3 N = planeNormal;

                #ifdef USING_DIRECTIONAL_LIGHT

                    float3 L =
                        normalize(_WorldSpaceLightPos0.xyz);

                    float atten = 1.0;

                #else

                    float3 L =
                        normalize(_WorldSpaceLightPos0.xyz - i.worldPos);

                    float atten = 1.0;

                #endif

                float3 diffuse =
                    _LightColor0.rgb *
                    max(0, dot(N, L));

                return fixed4(
                    _CapColor.rgb *
                    diffuse *
                    atten,
                    1);
            }

            ENDCG
        }

        // =========================================================
        // SHADOW CASTER
        // =========================================================

        Pass
        {
            Tags { "LightMode"="ShadowCaster" }

            Cull Back
            ZWrite On

            CGPROGRAM
            #pragma vertex vert_shadow
            #pragma fragment frag_shadow
            #pragma multi_compile_shadowcaster

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f_shadow
            {
                V2F_SHADOW_CASTER;
                float3 worldPos : TEXCOORD1;
            };

            float _PlanePosX;
            float _PlanePosY;
            float _PlanePosZ;

            float _PlaneNormX;
            float _PlaneNormY;
            float _PlaneNormZ;

            v2f_shadow vert_shadow(appdata v)
            {
                v2f_shadow o;

                TRANSFER_SHADOW_CASTER_NORMALOFFSET(o)

                o.worldPos =
                    mul(unity_ObjectToWorld, v.vertex).xyz;

                return o;
            }

            fixed4 frag_shadow(v2f_shadow i) : SV_Target
            {
                float3 planePos =
                    float3(_PlanePosX, _PlanePosY, _PlanePosZ);

                float3 planeNormal =
                    normalize(float3(
                        _PlaneNormX,
                        _PlaneNormY,
                        _PlaneNormZ));

                if (dot(i.worldPos - planePos, planeNormal) < 0)
                    discard;

                SHADOW_CASTER_FRAGMENT(i)
            }

            ENDCG
        }
    }
}