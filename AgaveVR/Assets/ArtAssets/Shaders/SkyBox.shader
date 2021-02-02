Shader "Custom/SkyBox"
{
    Properties
    {
        _MainTex ("Day Texture", Cube) = "white" {}
        _DayColorMult("Day Color Multiplier", Color) = (0,0,0,0)
        _SubTex ("Night Texture", 2D) = "white" {}

        _AlphaValue("Alpha", Range(0,1)) = 1
        _DayNightLerp ("Day Night Value", Range(0,1)) = 0
        _BlendAmnt ("Blend Amount", Float) = 0

        _Color1 ("Color 1", Color) = (1,1,1,1)
        _Color2 ("Color 2", Color) = (1,1,1,1)
        _Color3 ("Color 3", Color) = (1,1,1,1)
        _SmoothScalar("Smooth Scalar", Float) = 0
    }
    SubShader
    {

        Tags { "RenderType"="Transparent" }
        LOD 100
        Cull Front
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag


            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _SubTex;
            float4 _SubTex_ST;
            half _DayNightLerp;
            fixed4 _DayColorMult;
            half _AlphaValue;

            fixed4 _Color1;
            fixed4 _Color2;
            fixed4 _Color3;
            fixed _BlendAmnt;
            float _SmoothScalar;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                //fixed4 col = lerp(tex2D(_MainTex, i.uv) * _DayColorMult, tex2D(_SubTex, i.uv), _DayNightLerp);
                // Col2 = FFE400 col1 = 00F1FF
                //fixed4 sunsetBlend = smoothstep(_Color1 * i.uv.y, _Color2 * -i.uv.y, _SmoothScalar);
                //fixed4 col = i.uv.y > _DayNightLerp ? sunsetBlend * (1-i.uv.y * _BlendAmnt): sunsetBlend * (1- i.uv.y*_BlendAmnt);
                fixed4 col = smoothstep(_Color1 * (i.uv.y * _BlendAmnt), _Color2 * (1-i.uv.y), _SmoothScalar);
                col.a = _AlphaValue;
                return col;
            }
            ENDCG
        }
    }
}
