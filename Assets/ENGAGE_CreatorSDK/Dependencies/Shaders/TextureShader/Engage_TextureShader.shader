Shader "Engage/Texture"
{
	Properties
	{
		[Toggle] _SplitImages("Split images", int) = 0
		[KeywordEnum(None, Top_Bottom, Left_Right, Custom_UV)] _StereoMode("Stereo Mode", int) = 0

		_MainTex("Left Texture", 2D) = "white" {}
		_RightTex("Right Texture", 2D) = "white" {}

		_LeftEye("Left Eye Tiling", Vector) = (1,1,0,0)
		_RightEye("Right Eye Tiling", Vector) = (1,1,0,0)

		[Toggle]_ZWrite("ZWrite", int) = 1
		[Toggle]_Chroma("Chroma", int) = 0

		_ChromaKeyColor("Chroma Key Color", Color) = (0,1,0,1)
		_ChromaThreshold("Chroma Threshold", range(0.0, 1.0)) = 0.51
		_ChromaTolerance("Chroma Tolerance", range(0.0, 1.0)) = 0.05
	}

	CustomEditor "Engage_TextureShaderGUI"

	SubShader
	{
		Tags
		{
			"RenderType" = "Opaque"
		}

		LOD 100
		ZWrite[_ZWrite]

		Pass
		{
			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#pragma multi_compile FORCEEYE_NONE FORCEEYE_LEFT FORCEEYE_RIGHT

				#include "UnityCG.cginc"
				#include "EngageAVProVideo.cginc"
				#include "EngageCG.cginc"

				struct appdata
				{
					float4 vertex : POSITION;
					float2 uv : TEXCOORD0;
				};

				struct v2f
				{
					float2 uv : TEXCOORD0;
					float4 vertex : SV_POSITION;

					UNITY_FOG_COORDS(1)

					#if UNITY_SINGLE_PASS_STEREO
						UNITY_VERTEX_OUTPUT_STEREO
					#endif

					float stereoEyeIndex : TEXCOORD1;
				};

				bool _SplitImages;
				int _StereoMode;

				sampler2D _MainTex;
				float4 _MainTex_ST;

				sampler2D _RightTex;
				float4 _RightTex_ST;

				float4 _LeftEye;
				float4 _RightEye;

				bool _Chroma;
				half4 _ChromaKeyColor;
				half _ChromaThreshold;
				half _ChromaTolerance;

				v2f vert(appdata v)
				{
					v2f output;

					#if UNITY_SINGLE_PASS_STEREO
						output.stereoEyeIndex = 0;
						UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
					#else
						output.stereoEyeIndex = IsStereoEyeLeft() ? 0.0 : 1.0;
					#endif

					output.vertex = UnityObjectToClipPos(v.vertex);
					output.uv = TRANSFORM_TEX(v.uv, _MainTex);

					UNITY_TRANSFER_FOG(output, output.vertex);

					return output;
				}

				fixed4 frag(v2f i) : SV_Target
				{
					#if UNITY_SINGLE_PASS_STEREO
						UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);
					#endif

					fixed4 leftCol = float4(0,0,0,0);
					fixed4 rightCol = float4(0,0,0,0);

					if (_SplitImages)
					{
						leftCol = tex2D(_MainTex, i.uv);
						rightCol = tex2D(_RightTex, i.uv);
					}
					else
					{
						if (_StereoMode == 1) // No Stereo
						{
							_LeftEye = float4(1, 1, 0, 0);
							_RightEye = float4(1, 1, 0, 0);
						}
						else if (_StereoMode == 1) // Top - Bottom Stereo
						{
							_LeftEye = float4(1, 0.5f, 0, 0);
							_RightEye = float4(1, 0.5f, 0, 0.5f);
						}
						else if (_StereoMode == 2) // Left - Right Stereo
						{
							_LeftEye = float4(0.5f, 1, 0, 0);
							_RightEye = float4(0.5f, 1, 0.5f, 0);
						}

						leftCol = tex2D(_MainTex, i.uv * _LeftEye.xy + _LeftEye.zw);
						rightCol = tex2D(_MainTex, i.uv * _RightEye.xy + _RightEye.zw);
					}

					#if UNITY_SINGLE_PASS_STEREO
						fixed4 c = lerp(leftCol, rightCol, unity_StereoEyeIndex);
					#else
						fixed4 c = lerp(leftCol, rightCol, i.stereoEyeIndex);
					#endif

					if (_Chroma)
					{
						c = applyChroma(c, _ChromaKeyColor, _ChromaThreshold, _ChromaTolerance);

						if (c.a < 0.5) discard;
					}

					UNITY_APPLY_FOG(i.fogCoord, c);

					return c;
				}
			ENDCG
		}
	}
}
