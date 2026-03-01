Shader "Custom/Lights_PPS"
{   
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

            float4 Frag (Varyings input) : SV_Target
            {
                float4 color = SAMPLE_TEXTURE2D(_BlitTexture, sampler_LinearClamp, input.texcoord).rgba;
                return float4(1, 0.8, 0.2, 1)*color+(1-float4(1, 0.4, 0, 1))*0.005f; 
            }
            ENDHLSL
        }
    }
}
