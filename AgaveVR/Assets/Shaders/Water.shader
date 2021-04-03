Shader "Custom/Water"
{
    Properties
    {
        _PrimaryNormal("Primary Normal Map", 2D) = "bump" {}
        _SecondaryNormal("Secondary Normal Map", 2D) = "bump" {}

        _Color("Primary Color", Color) = (1,1,1,1)

        _ScrollSpeedDamper("Scroll Speed Damper", Float) = 1
    }

    SubShader
    {
        Tags { "RenderType" = "Transparent" "Queue" = "Transparent"}
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            #include "Lighting.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float2 uv2 : TEXCOORD1;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                float2 uv2 : TEXCOORD1;
                float3 viewDir : TEXCOORD2;
                float3 tangentSpaceLight : TEXCOORD3;
            };

            sampler2D _PrimaryNormal;
            sampler2D _SecondaryNormal;
            float4 _PrimaryNormal_ST;
            float4 _SecondaryNormal_ST;

            half _ScrollSpeedDamper;

            fixed4 _Color;

            v2f vert (appdata v)
            {
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_TRANSFER_INSTANCE_ID(v, o);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

                o.vertex = UnityObjectToClipPos(v.vertex);

                v.uv.y += _Time.y / _ScrollSpeedDamper;
                v.uv2.y += _Time.y / (_ScrollSpeedDamper / 3);

                o.uv = TRANSFORM_TEX(v.uv, _PrimaryNormal);
                o.uv2 = TRANSFORM_TEX(v.uv2, _SecondaryNormal);

                float3 normal = UnityObjectToWorldNormal(v.normal);
                float3 tangent = UnityObjectToWorldNormal(v.tangent);
                float3 bitangent = cross(tangent, normal);

                o.tangentSpaceLight = float3
                    (
                        dot(tangent, _WorldSpaceLightPos0),
                        dot(bitangent, _WorldSpaceLightPos0),
                        dot(normal, _WorldSpaceLightPos0)
                    );
                
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                UNITY_SETUP_INSTANCE_ID(i);
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);

                half2 ptNormal = UnpackNormal(tex2D(_PrimaryNormal, i.uv));
                half2 stNormal = UnpackNormal(tex2D(_SecondaryNormal, i.uv2));

                fixed4 col = _Color;
                return col + (saturate(dot(ptNormal * stNormal, i.tangentSpaceLight)));
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}