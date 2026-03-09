Shader "Custom/Lights_PPS"
{   
    Properties
    {
        _ColorA ("Color A", Color) = (1,0,0,1)
        _ColorB ("Color B", Color) = (0,1,0,1)
        _ColorC ("Color C", Color) = (0,0,1,1)
    }

    SubShader
    {
        HLSLINCLUDE
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.core/Runtime/Utilities/Blit.hlsl"
        ENDHLSL

        Tags { "RenderType"="Opaque" }
        LOD 100
        ZWrite Off Cull Off

        Pass
        {
            Name "Lights_PPS"

            HLSLPROGRAM
            
            #pragma vertex Vert
            #pragma fragment Frag

            float4 _ColorA;
            float4 _ColorB;
            float4 _ColorC;

            float4 Frag (Varyings input) : SV_Target
            {
                float4 color = SAMPLE_TEXTURE2D(_BlitTexture, sampler_LinearClamp, input.texcoord).rgba;
                float3 adjusted = color.rgb;
                return adjusted.x * _ColorA + adjusted.y * _ColorB + adjusted.z * _ColorC;
                //return 1;
            }
            ENDHLSL
        }
    }
}
