#ifndef __DEPTH_OF_FIELD__
#define __DEPTH_OF_FIELD__

<<<<<<< HEAD
#include "UnityCG.cginc"
#include "Common.cginc"
#include "DiskKernels.cginc"

#define PREFILTER_LUMA_WEIGHT 1

sampler2D_float _CameraDepthTexture;
sampler2D_float _HistoryCoC;
float _HistoryWeight;
=======
#if SHADER_TARGET >= 50
    // Use separate texture/sampler objects on Shader Model 5.0
    #define SEPARATE_TEXTURE_SAMPLER
    #define DOF_DECL_TEX2D(tex) Texture2D tex; SamplerState sampler##tex
    #define DOF_TEX2D(tex, coord) tex.Sample(sampler##tex, coord)
#else
    #define DOF_DECL_TEX2D(tex) sampler2D tex
    #define DOF_TEX2D(tex, coord) tex2D(tex, coord)
#endif

#include "Common.cginc"
#include "DiskKernels.cginc"

DOF_DECL_TEX2D(_CameraDepthTexture);
DOF_DECL_TEX2D(_CameraMotionVectorsTexture);
DOF_DECL_TEX2D(_CoCTex);
>>>>>>> 4a27596bb8cec86431ec3eabbef194b0b6e9967c

// Camera parameters
float _Distance;
float _LensCoeff;  // f^2 / (N * (S1 - f) * film_width * 2)
float _MaxCoC;
float _RcpMaxCoC;
float _RcpAspect;
<<<<<<< HEAD
=======
half3 _TaaParams; // Jitter.x, Jitter.y, Blending
>>>>>>> 4a27596bb8cec86431ec3eabbef194b0b6e9967c

struct VaryingsDOF
{
    float4 pos : SV_POSITION;
    half2 uv : TEXCOORD0;
    half2 uvAlt : TEXCOORD1;
};

// Common vertex shader with single pass stereo rendering support
VaryingsDOF VertDOF(AttributesDefault v)
{
    half2 uvAlt = v.texcoord;
#if UNITY_UV_STARTS_AT_TOP
    if (_MainTex_TexelSize.y < 0.0) uvAlt.y = 1.0 - uvAlt.y;
#endif

    VaryingsDOF o;
    o.pos = UnityObjectToClipPos(v.vertex);

#if defined(UNITY_SINGLE_PASS_STEREO)
    o.uv = UnityStereoScreenSpaceUVAdjust(v.texcoord, _MainTex_ST);
    o.uvAlt = UnityStereoScreenSpaceUVAdjust(uvAlt, _MainTex_ST);
#else
    o.uv = v.texcoord;
    o.uvAlt = uvAlt;
#endif

    return o;
}

<<<<<<< HEAD
// Prefilter: CoC calculation, downsampling and premultiplying.

#if defined(PREFILTER_TAA)

// TAA enabled: use MRT to update the history buffer in the same pass.
struct PrefilterOutput
{
    half4 base : SV_Target0;
    half4 history : SV_Target1;
};
#define PrefilterSemantics

#else

// No TAA
#define PrefilterOutput half4
#define PrefilterSemantics :SV_Target

#endif

PrefilterOutput FragPrefilter(VaryingsDOF i) PrefilterSemantics
{
    float3 duv = _MainTex_TexelSize.xyx * float3(0.5, 0.5, -0.5);

    // Sample source colors.
    half3 c0 = tex2D(_MainTex, i.uv - duv.xy).rgb;
    half3 c1 = tex2D(_MainTex, i.uv - duv.zy).rgb;
    half3 c2 = tex2D(_MainTex, i.uv + duv.zy).rgb;
    half3 c3 = tex2D(_MainTex, i.uv + duv.xy).rgb;

    // Sample linear depths.
    float d0 = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, i.uvAlt - duv.xy));
    float d1 = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, i.uvAlt - duv.zy));
    float d2 = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, i.uvAlt + duv.zy));
    float d3 = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, i.uvAlt + duv.xy));
    float4 depths = float4(d0, d1, d2, d3);

    // Calculate the radiuses of CoCs at these sample points.
    float4 cocs = (depths - _Distance) * _LensCoeff / depths;
    cocs = clamp(cocs, -_MaxCoC, _MaxCoC);

#if defined(PREFILTER_TAA)
    // Get the average with the history to avoid temporal aliasing.
    half hcoc = tex2D(_HistoryCoC, i.uv).r;
    cocs = lerp(cocs, hcoc, _HistoryWeight);
#endif

    // Premultiply CoC to reduce background bleeding.
    float4 weights = saturate(abs(cocs) * _RcpMaxCoC);

#if defined(PREFILTER_LUMA_WEIGHT)
    // Apply luma weights to reduce flickering.
    // References:
    //   http://gpuopen.com/optimized-reversible-tonemapper-for-resolve/
    //   http://graphicrants.blogspot.fr/2013/12/tone-mapping.html
    weights.x *= 1.0 / (Max3(c0) + 1.0);
    weights.y *= 1.0 / (Max3(c1) + 1.0);
    weights.z *= 1.0 / (Max3(c2) + 1.0);
    weights.w *= 1.0 / (Max3(c3) + 1.0);
#endif

    // Weighted average of the color samples
    half3 avg = c0 * weights.x + c1 * weights.y + c2 * weights.z + c3 * weights.w;
    avg /= dot(weights, 1.0);

    // Output CoC = average of CoCs
    half cocmin = Min4(cocs);
    half cocmax = Max4(cocs);
    half coc = -cocmin > cocmax ? cocmin : cocmax;
=======
// CoC calculation
half4 FragCoC(VaryingsDOF i) : SV_Target
{
    float depth = LinearEyeDepth(DOF_TEX2D(_CameraDepthTexture, i.uv));
    half coc = (depth - _Distance) * _LensCoeff / max(depth, 1e-5);
    return saturate(coc * 0.5 * _RcpMaxCoC + 0.5);
}

// Temporal filter
half4 FragTempFilter(VaryingsDOF i) : SV_Target
{
    float3 uvOffs = _MainTex_TexelSize.xyy * float3(1, 1, 0);

#if defined(SEPARATE_TEXTURE_SAMPLER)

    half4 cocTL = _CoCTex.GatherRed(sampler_CoCTex, i.uv - uvOffs.xy * 0.5); // top-left
    half4 cocBR = _CoCTex.GatherRed(sampler_CoCTex, i.uv + uvOffs.xy * 0.5); // bottom-right
    half coc1 = cocTL.x; // top
    half coc2 = cocTL.z; // left
    half coc3 = cocBR.x; // bottom
    half coc4 = cocBR.z; // right

#else

    half coc1 = DOF_TEX2D(_CoCTex, i.uv - uvOffs.xz).r; // top
    half coc2 = DOF_TEX2D(_CoCTex, i.uv - uvOffs.zy).r; // left
    half coc3 = DOF_TEX2D(_CoCTex, i.uv + uvOffs.zy).r; // bottom
    half coc4 = DOF_TEX2D(_CoCTex, i.uv + uvOffs.xz).r; // right

#endif

    // Dejittered center sample.
    half coc0 = DOF_TEX2D(_CoCTex, i.uv - _TaaParams.xy).r;

    // CoC dilation: determine the closest point in the four neighbors.
    float3 closest = float3(0, 0, coc0);
    closest = coc1 < closest.z ? float3(-uvOffs.xz, coc1) : closest;
    closest = coc2 < closest.z ? float3(-uvOffs.zy, coc2) : closest;
    closest = coc3 < closest.z ? float3(+uvOffs.zy, coc3) : closest;
    closest = coc4 < closest.z ? float3(+uvOffs.xz, coc4) : closest;

    // Sample the history buffer with the motion vector at the closest point.
    float2 motion = DOF_TEX2D(_CameraMotionVectorsTexture, i.uv + closest.xy).xy;
    half cocHis = DOF_TEX2D(_MainTex, i.uv - motion).r;

    // Neighborhood clamping.
    half cocMin = closest.z;
    half cocMax = max(max(max(max(coc0, coc1), coc2), coc3), coc4);
    cocHis = clamp(cocHis, cocMin, cocMax);

    // Blend with the history.
    return lerp(coc0, cocHis, _TaaParams.z);
}

// Prefilter: downsampling and premultiplying.
half4 FragPrefilter(VaryingsDOF i) : SV_Target
{
#if defined(SEPARATE_TEXTURE_SAMPLER)

    // Sample source colors.
    half4 c_r = _MainTex.GatherRed  (sampler_MainTex, i.uv);
    half4 c_g = _MainTex.GatherGreen(sampler_MainTex, i.uv);
    half4 c_b = _MainTex.GatherBlue (sampler_MainTex, i.uv);

    half3 c0 = half3(c_r.x, c_g.x, c_b.x);
    half3 c1 = half3(c_r.y, c_g.y, c_b.y);
    half3 c2 = half3(c_r.z, c_g.z, c_b.z);
    half3 c3 = half3(c_r.w, c_g.w, c_b.w);

    // Sample CoCs.
    half4 cocs = _CoCTex.Gather(sampler_CoCTex, i.uvAlt) * 2.0 - 1.0;
    half coc0 = cocs.x;
    half coc1 = cocs.y;
    half coc2 = cocs.z;
    half coc3 = cocs.w;

#else

    float3 duv = _MainTex_TexelSize.xyx * float3(0.5, 0.5, -0.5);

    // Sample source colors.
    half3 c0 = DOF_TEX2D(_MainTex, i.uv - duv.xy).rgb;
    half3 c1 = DOF_TEX2D(_MainTex, i.uv - duv.zy).rgb;
    half3 c2 = DOF_TEX2D(_MainTex, i.uv + duv.zy).rgb;
    half3 c3 = DOF_TEX2D(_MainTex, i.uv + duv.xy).rgb;

    // Sample CoCs.
    half coc0 = DOF_TEX2D(_CoCTex, i.uvAlt - duv.xy).r * 2.0 - 1.0;
    half coc1 = DOF_TEX2D(_CoCTex, i.uvAlt - duv.zy).r * 2.0 - 1.0;
    half coc2 = DOF_TEX2D(_CoCTex, i.uvAlt + duv.zy).r * 2.0 - 1.0;
    half coc3 = DOF_TEX2D(_CoCTex, i.uvAlt + duv.xy).r * 2.0 - 1.0;

#endif

    // Apply CoC and luma weights to reduce bleeding and flickering.
    float w0 = abs(coc0) / (Max3(c0) + 1.0);
    float w1 = abs(coc1) / (Max3(c1) + 1.0);
    float w2 = abs(coc2) / (Max3(c2) + 1.0);
    float w3 = abs(coc3) / (Max3(c3) + 1.0);

    // Weighted average of the color samples
    half3 avg = c0 * w0 + c1 * w1 + c2 * w2 + c3 * w3;
    avg /= max(w0 + w1 + w2 + w3, 1e-5);

    // Select the largest CoC value.
    half coc_min = Min4(coc0, coc1, coc2, coc3);
    half coc_max = Max4(coc0, coc1, coc2, coc3);
    half coc = (-coc_min > coc_max ? coc_min : coc_max) * _MaxCoC;
>>>>>>> 4a27596bb8cec86431ec3eabbef194b0b6e9967c

    // Premultiply CoC again.
    avg *= smoothstep(0, _MainTex_TexelSize.y * 2, abs(coc));

#if defined(UNITY_COLORSPACE_GAMMA)
    avg = GammaToLinearSpace(avg);
#endif

<<<<<<< HEAD
#if defined(PREFILTER_TAA)
    PrefilterOutput output;
    output.base = half4(avg, coc);
    output.history = coc.xxxx;
    return output;
#else
    return half4(avg, coc);
#endif
=======
    return half4(avg, coc);
>>>>>>> 4a27596bb8cec86431ec3eabbef194b0b6e9967c
}

// Bokeh filter with disk-shaped kernels
half4 FragBlur(VaryingsDOF i) : SV_Target
{
<<<<<<< HEAD
    half4 samp0 = tex2D(_MainTex, i.uv);
=======
    half4 samp0 = DOF_TEX2D(_MainTex, i.uv);
>>>>>>> 4a27596bb8cec86431ec3eabbef194b0b6e9967c

    half4 bgAcc = 0.0; // Background: far field bokeh
    half4 fgAcc = 0.0; // Foreground: near field bokeh

    UNITY_LOOP for (int si = 0; si < kSampleCount; si++)
    {
        float2 disp = kDiskKernel[si] * _MaxCoC;
        float dist = length(disp);

        float2 duv = float2(disp.x * _RcpAspect, disp.y);
<<<<<<< HEAD
        half4 samp = tex2D(_MainTex, i.uv + duv);
=======
        half4 samp = DOF_TEX2D(_MainTex, i.uv + duv);
>>>>>>> 4a27596bb8cec86431ec3eabbef194b0b6e9967c

        // BG: Compare CoC of the current sample and the center sample
        // and select smaller one.
        half bgCoC = max(min(samp0.a, samp.a), 0.0);

        // Compare the CoC to the sample distance.
        // Add a small margin to smooth out.
        const half margin = _MainTex_TexelSize.y * 2;
        half bgWeight = saturate((bgCoC   - dist + margin) / margin);
        half fgWeight = saturate((-samp.a - dist + margin) / margin);

        // Cut influence from focused areas because they're darkened by CoC
        // premultiplying. This is only needed for near field.
        fgWeight *= step(_MainTex_TexelSize.y, -samp.a);

        // Accumulation
        bgAcc += half4(samp.rgb, 1.0) * bgWeight;
        fgAcc += half4(samp.rgb, 1.0) * fgWeight;
    }

    // Get the weighted average.
    bgAcc.rgb /= bgAcc.a + (bgAcc.a == 0.0); // zero-div guard
    fgAcc.rgb /= fgAcc.a + (fgAcc.a == 0.0);

    // BG: Calculate the alpha value only based on the center CoC.
    // This is a rather aggressive approximation but provides stable results.
    bgAcc.a = smoothstep(_MainTex_TexelSize.y, _MainTex_TexelSize.y * 2.0, samp0.a);

    // FG: Normalize the total of the weights.
    fgAcc.a *= UNITY_PI / kSampleCount;

    // Alpha premultiplying
<<<<<<< HEAD
    half3 rgb = 0.0;
    rgb = lerp(rgb, bgAcc.rgb, saturate(bgAcc.a));
    rgb = lerp(rgb, fgAcc.rgb, saturate(fgAcc.a));

    // Combined alpha value
    half alpha = (1.0 - saturate(bgAcc.a)) * (1.0 - saturate(fgAcc.a));
=======
    half alpha = saturate(fgAcc.a);
    half3 rgb = lerp(bgAcc.rgb, fgAcc.rgb, alpha);
>>>>>>> 4a27596bb8cec86431ec3eabbef194b0b6e9967c

    return half4(rgb, alpha);
}

// Postfilter blur
half4 FragPostBlur(VaryingsDOF i) : SV_Target
{
<<<<<<< HEAD
    // 9-tap tent filter
    float4 duv = _MainTex_TexelSize.xyxy * float4(1, 1, -1, 0);

    half4 c0 = tex2D(_MainTex, i.uv - duv.xy);
    half4 c1 = tex2D(_MainTex, i.uv - duv.wy);
    half4 c2 = tex2D(_MainTex, i.uv - duv.zy);

    half4 c3 = tex2D(_MainTex, i.uv + duv.zw);
    half4 c4 = tex2D(_MainTex, i.uv         );
    half4 c5 = tex2D(_MainTex, i.uv + duv.xw);

    half4 c6 = tex2D(_MainTex, i.uv + duv.zy);
    half4 c7 = tex2D(_MainTex, i.uv + duv.wy);
    half4 c8 = tex2D(_MainTex, i.uv + duv.xy);

    half4 acc = c0 * 1 + c1 * 2 + c2 * 1 +
                c3 * 2 + c4 * 4 + c5 * 2 +
                c6 * 1 + c7 * 2 + c8 * 1;

    half aa =
        c0.a * c0.a * 1 + c1.a * c1.a * 2 + c2.a * c2.a * 1 +
        c3.a * c3.a * 2 + c4.a * c4.a * 4 + c5.a * c5.a * 2 +
        c6.a * c6.a * 1 + c7.a * c7.a * 2 + c8.a * c8.a * 1;

    half wb = 1.2;
    half a = (wb * acc.a - aa) / (wb * 16 - acc.a);

    acc /= 16;

    half3 rgb = acc.rgb * (1 + saturate(acc.a - a));
    return half4(rgb, a);
=======
    // 9 tap tent filter with 4 bilinear samples
    const float4 duv = _MainTex_TexelSize.xyxy * float4(0.5, 0.5, -0.5, 0);
    half4 acc;
    acc  = DOF_TEX2D(_MainTex, i.uv - duv.xy);
    acc += DOF_TEX2D(_MainTex, i.uv - duv.zy);
    acc += DOF_TEX2D(_MainTex, i.uv + duv.zy);
    acc += DOF_TEX2D(_MainTex, i.uv + duv.xy);
    return acc / 4.0;
>>>>>>> 4a27596bb8cec86431ec3eabbef194b0b6e9967c
}

#endif // __DEPTH_OF_FIELD__
