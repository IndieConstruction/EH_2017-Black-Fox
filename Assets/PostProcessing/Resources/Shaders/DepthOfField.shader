Shader "Hidden/Post FX/Depth Of Field"
{
    Properties
    {
        _MainTex ("", 2D) = "black"
    }

    CGINCLUDE
        #pragma exclude_renderers d3d11_9x
<<<<<<< HEAD
        #pragma target 3.0
    ENDCG

=======
    ENDCG

    // SubShader with SM 5.0 support
    // Gather intrinsics are used to reduce texture sample count.
>>>>>>> 4a27596bb8cec86431ec3eabbef194b0b6e9967c
    SubShader
    {
        Cull Off ZWrite Off ZTest Always

<<<<<<< HEAD
        // (0) Downsampling, prefiltering & CoC
        Pass
        {
            CGPROGRAM
                #pragma multi_compile __ UNITY_COLORSPACE_GAMMA
                #pragma vertex VertDOF
                #pragma fragment FragPrefilter
=======
        Pass // 0
        {
            Name "CoC Calculation"
            CGPROGRAM
                #pragma target 3.0
                #pragma vertex VertDOF
                #pragma fragment FragCoC
                #include "DepthOfField.cginc"
            ENDCG
        }

        Pass // 1
        {
            Name "CoC Temporal Filter"
            CGPROGRAM
                #pragma target 5.0
                #pragma vertex VertDOF
                #pragma fragment FragTempFilter
                #include "DepthOfField.cginc"
            ENDCG
        }

        Pass // 2
        {
            Name "Downsample and Prefilter"
            CGPROGRAM
                #pragma target 5.0
                #pragma vertex VertDOF
                #pragma fragment FragPrefilter
                #pragma multi_compile __ UNITY_COLORSPACE_GAMMA
                #include "DepthOfField.cginc"
            ENDCG
        }

        Pass // 3
        {
            Name "Bokeh Filter (small)"
            CGPROGRAM
                #pragma target 3.0
                #pragma vertex VertDOF
                #pragma fragment FragBlur
                #define KERNEL_SMALL
                #include "DepthOfField.cginc"
            ENDCG
        }

        Pass // 4
        {
            Name "Bokeh Filter (medium)"
            CGPROGRAM
                #pragma target 3.0
                #pragma vertex VertDOF
                #pragma fragment FragBlur
                #define KERNEL_MEDIUM
                #include "DepthOfField.cginc"
            ENDCG
        }

        Pass // 5
        {
            Name "Bokeh Filter (large)"
            CGPROGRAM
                #pragma target 3.0
                #pragma vertex VertDOF
                #pragma fragment FragBlur
                #define KERNEL_LARGE
                #include "DepthOfField.cginc"
            ENDCG
        }

        Pass // 6
        {
            Name "Bokeh Filter (very large)"
            CGPROGRAM
                #pragma target 3.0
                #pragma vertex VertDOF
                #pragma fragment FragBlur
                #define KERNEL_VERYLARGE
                #include "DepthOfField.cginc"
            ENDCG
        }

        Pass // 7
        {
            Name "Postfilter"
            CGPROGRAM
                #pragma target 3.0
                #pragma vertex VertDOF
                #pragma fragment FragPostBlur
                #include "DepthOfField.cginc"
            ENDCG
        }
    }

    // Fallback SubShader with SM 3.0
    SubShader
    {
        Cull Off ZWrite Off ZTest Always

        Pass // 0
        {
            Name "CoC Calculation"
            CGPROGRAM
                #pragma target 3.0
                #pragma vertex VertDOF
                #pragma fragment FragCoC
                #include "DepthOfField.cginc"
            ENDCG
        }

        Pass // 1
        {
            Name "CoC Temporal Filter"
            CGPROGRAM
                #pragma target 3.0
                #pragma vertex VertDOF
                #pragma fragment FragTempFilter
>>>>>>> 4a27596bb8cec86431ec3eabbef194b0b6e9967c
                #include "DepthOfField.cginc"
            ENDCG
        }

<<<<<<< HEAD
        // (1) Pass 0 + temporal antialiasing
        Pass
        {
            CGPROGRAM
                #pragma vertex VertDOF
                #pragma fragment FragPrefilter
                #define PREFILTER_TAA
=======
        Pass // 2
        {
            Name "Downsample and Prefilter"
            CGPROGRAM
                #pragma target 3.0
                #pragma vertex VertDOF
                #pragma fragment FragPrefilter
                #pragma multi_compile __ UNITY_COLORSPACE_GAMMA
>>>>>>> 4a27596bb8cec86431ec3eabbef194b0b6e9967c
                #include "DepthOfField.cginc"
            ENDCG
        }

<<<<<<< HEAD
        // (2-5) Bokeh filter with disk-shaped kernels
        Pass
        {
            CGPROGRAM
=======
        Pass // 3
        {
            Name "Bokeh Filter (small)"
            CGPROGRAM
                #pragma target 3.0
>>>>>>> 4a27596bb8cec86431ec3eabbef194b0b6e9967c
                #pragma vertex VertDOF
                #pragma fragment FragBlur
                #define KERNEL_SMALL
                #include "DepthOfField.cginc"
            ENDCG
        }

<<<<<<< HEAD
        Pass
        {
            CGPROGRAM
=======
        Pass // 4
        {
            Name "Bokeh Filter (medium)"
            CGPROGRAM
                #pragma target 3.0
>>>>>>> 4a27596bb8cec86431ec3eabbef194b0b6e9967c
                #pragma vertex VertDOF
                #pragma fragment FragBlur
                #define KERNEL_MEDIUM
                #include "DepthOfField.cginc"
            ENDCG
        }

<<<<<<< HEAD
        Pass
        {
            CGPROGRAM
=======
        Pass // 5
        {
            Name "Bokeh Filter (large)"
            CGPROGRAM
                #pragma target 3.0
>>>>>>> 4a27596bb8cec86431ec3eabbef194b0b6e9967c
                #pragma vertex VertDOF
                #pragma fragment FragBlur
                #define KERNEL_LARGE
                #include "DepthOfField.cginc"
            ENDCG
        }

<<<<<<< HEAD
        Pass
        {
            CGPROGRAM
=======
        Pass // 6
        {
            Name "Bokeh Filter (very large)"
            CGPROGRAM
                #pragma target 3.0
>>>>>>> 4a27596bb8cec86431ec3eabbef194b0b6e9967c
                #pragma vertex VertDOF
                #pragma fragment FragBlur
                #define KERNEL_VERYLARGE
                #include "DepthOfField.cginc"
            ENDCG
        }

<<<<<<< HEAD
        // (6) Postfilter blur
        Pass
        {
            CGPROGRAM
=======
        Pass // 7
        {
            Name "Postfilter"
            CGPROGRAM
                #pragma target 3.0
>>>>>>> 4a27596bb8cec86431ec3eabbef194b0b6e9967c
                #pragma vertex VertDOF
                #pragma fragment FragPostBlur
                #include "DepthOfField.cginc"
            ENDCG
        }
    }

    FallBack Off
}
