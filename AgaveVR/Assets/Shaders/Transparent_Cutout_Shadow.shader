Shader "Unlit/Transparent Cutout (Shadow)"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
        _SecondaryTex("Secondary Tex", 2D) = "white" {}
        _Cutoff  ("Alpha cutoff", Range(0,1)) = 0.5
        _ScrollSpeedDamper("Scroll Speed Damper", Float) = 1
    }
    SubShader
    {
        Tags
        {
            "Queue" = "AlphaTest"
            "IgnoreProjector" = "True"
            "RenderType" = "TransparentCutout"
        }
        LOD      100
        Lighting Off
 
        CGPROGRAM
        #pragma surface surf Lambert alphatest:_Cutoff
 
        sampler2D _MainTex;
        //float4 _MainTex_ST;
        sampler2D _SecondaryTex;
        //float4 _SecondaryTex_ST;
        fixed _ScrollSpeedDamper;
        fixed4 _Color;
 
        struct Input
        {
            float2 uv_MainTex;
            float2 uv_SecondaryTex;
            fixed _ScrollSpeedDamper;
        };
        
        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed2 mainUVScroll = IN.uv_MainTex;
            fixed2 secUVScroll = IN.uv_SecondaryTex;

            mainUVScroll.y += _Time.y / _ScrollSpeedDamper;
            secUVScroll.xy += _Time.y / (_ScrollSpeedDamper / 3);

            fixed4 c = tex2D(_MainTex, mainUVScroll) * tex2D(_SecondaryTex, secUVScroll);
            o.Albedo = c * _Color;
            o.Alpha = c.a;
        }
        ENDCG
    }
 
    Fallback "Transparent/Cutout/VertexLit"
}