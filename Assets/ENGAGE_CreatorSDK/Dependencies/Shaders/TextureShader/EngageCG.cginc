#if defined (SHADERLAB_GLSL)
	#define INLINE 
	#define HALF float
	#define HALF2 vec2
	#define HALF3 vec3
	#define HALF4 vec4
	#define FLOAT float
	#define FLOAT2 vec2
	#define FLOAT3 vec3
	#define FLOAT4 vec4
	#define FLOAT3X3 mat3
	#define FLOAT4X4 mat4
	#define LERP mix
#else
	#define INLINE inline
	#define HALF half
	#define HALF2 half2
	#define HALF3 half3
	#define HALF4 half4
	#define FLOAT float
	#define FLOAT2 float2
	#define FLOAT3 float3
	#define FLOAT4 float4
	#define FLOAT3X3 float3x3
	#define FLOAT4X4 float4x4
	#define LERP lerp
#endif

INLINE FLOAT2 aspectRatioUVs(FLOAT2 uv, FLOAT4 texST, FLOAT4 texelSize, FLOAT targetAspectRatio, FLOAT fitFillFactor)
{
	FLOAT aspectRatio = texelSize.z / texelSize.w;

	// Tweak for double 16:9 stereo images either side by side or top bottom.
	FLOAT ratioFactor = aspectRatio / targetAspectRatio;
	bool isDouble = ratioFactor == 2.0 || ratioFactor == 0.5;
	aspectRatio /= isDouble ? ratioFactor : 1.0;

	FLOAT xRatio = targetAspectRatio / aspectRatio;
	FLOAT yRatio = 1.0 / xRatio;

	fitFillFactor = abs(fitFillFactor - step(aspectRatio, targetAspectRatio));

	xRatio = LERP(1.0, xRatio, fitFillFactor);
	yRatio = LERP(yRatio, 1.0, fitFillFactor);

	uv.x *= xRatio * texST.x;
	uv.x += texST.z - (texST.x * (xRatio - 1.0) / 2.0);
	uv.y *= yRatio * texST.y;
	uv.y += texST.w - (texST.y * (yRatio - 1.0) / 2.0);

	return uv;
}

INLINE HALF4 aspectRatioColor(FLOAT2 uv, FLOAT4 texST, HALF4 color, HALF4 fillColor, bool chroma)
{
	fillColor.a = chroma ? 0.0 : fillColor.a;
	uv = (uv - texST.zw) / texST.xy;
	FLOAT inside = step(-0.001, uv.x) * step(uv.x, 1.001) * step(-0.001, uv.y) * step(uv.y, 1.001);

	return LERP(fillColor, color, inside);
}

INLINE HALF3 RGB_To_YCbCr(HALF3 rgb)
{
	HALF Y = 0.299 * rgb.r + 0.587 * rgb.g + 0.114 * rgb.b;
	HALF Cb = 0.564 * (rgb.b - Y);
	HALF Cr = 0.713 * (rgb.r - Y);

	return HALF3(Cb, Cr, Y);
}

INLINE HALF4 applyChroma(HALF4 color, HALF4 keyColor, FLOAT threshold, FLOAT tolerance)
{
	HALF3 src_YCbCr = RGB_To_YCbCr(color.rgb);
	HALF3 key_YCbCr = RGB_To_YCbCr(keyColor.rgb);

	FLOAT chromaDist = distance(src_YCbCr.xy, key_YCbCr.xy);

	FLOAT alphaMix = step(chromaDist, threshold);
	FLOAT chromaMix = step(threshold - tolerance, chromaDist);

	FLOAT sourceAlpha = color.a;
	FLOAT chromaAlpha = (chromaDist - threshold + tolerance) / tolerance;
	chromaAlpha = chromaAlpha * chromaMix;
	chromaAlpha = LERP(sourceAlpha, chromaAlpha, step(chromaAlpha, sourceAlpha));

	color.a = LERP(sourceAlpha, chromaAlpha, alphaMix);
	
	return color;
}

INLINE HALF4 correctColorSpace(HALF4 color)
{
    #ifndef UNITY_COLORSPACE_GAMMA
    color.rgb = color.rgb * (color.rgb * (color.rgb * 0.305306011 + 0.682171111) + 0.012522878); // GammaToLinear
    #endif
    return color;
}

