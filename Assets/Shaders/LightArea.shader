Shader "Custom/LightArea"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100

        Cull Front
        ZWrite Off
        Blend SrcAlpha One

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma Lambert alpha

            #include "UnityCG.cginc"


            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 normal : NORMAL;
                float3 worldPos : TEXCOORD1;
                float3 pdiscance : TEXCOORD2;
                float3 ray : TEXCOORD3;
            };

            fixed4 _Color;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.normal = UnityObjectToWorldNormal(v.normal);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                float3 origin = mul(unity_ObjectToWorld, float4(0,0,0,1)).xyz;
                o.ray = o.worldPos - _WorldSpaceCameraPos;
                o.pdiscance = 1-dot(normalize(o.worldPos - origin),normalize(o.ray));
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = _Color;
                //float heightFactor = clamp(i.worldPos.y*3,0,1);
                float clipFactor = clamp(length(i.ray)/3-0.5,0,1);
                float thicknessFactor = clamp((((1.1/(pow(i.pdiscance,2)+0.1))-1)-0.1)/100 ,0,1);
                //float thicknessFactor = clamp(pow(i.pdiscance,2),0,1);
                //float thicknessFactor = clamp(pow(1-abs(dot(i.normal, normalize(ray))),4),0,1);
                col.a = 0.2*thicknessFactor*clipFactor;//*heightFactor;
                //return fixed4(1,0,0.8,1);
                return col;
            }
            ENDCG
        }
    }
}
