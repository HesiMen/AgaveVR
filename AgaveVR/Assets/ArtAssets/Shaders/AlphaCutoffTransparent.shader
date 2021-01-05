Shader "Agave/Unlit/AlphaCutOffTransparent"
{
    Properties
    {
        [Toggle(MAINTEX_ON)] _MainTexToggle("MainTexToggle", Float) = 0
        _MainTex ("Texture", 2D) = "white" {}
        _Color("Color", Color) = (1,1,1,1)
        _Alpha ("Alpha", Range(0,1)) = 1
        [Toggle(CUTOFF_ON)] _CutoffToggle("_CutOff Toggle", Float) = 0
        _Cutoff("Alpha Cutoff", Range(0,1)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent"}
        LOD 100

        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma shader_feature CUTOFF_OFF CUTOFF_ON
            #pragma shader_feature MAINTEX_OFF MAINTEX_ON

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                fixed4 color : COLOR;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                fixed4 color : COLOR;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            half4 _Color; 
            fixed _Cutoff;
            half _Alpha;

            //Toggle Items
            fixed _CutoffToggle;
            fixed _MainTexToggle;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.color = v.color * _Color;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = _Color;
                // sample the texture
                #if MAINTEX_ON
                    col.rgba = tex2D(_MainTex, i.uv).rgba * i.color;
                #endif

                #if CUTOFF_ON
                    clip(col.a - (_Cutoff * _CutoffToggle));
                #endif

                //col.a = _Alpha;

                //tex2D(_MainTex, i.uv);
                return col;
            }
            ENDCG
        }
    }
}
