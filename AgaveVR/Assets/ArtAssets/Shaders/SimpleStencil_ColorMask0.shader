Shader "Custom/SimpleStencil"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        [Enum(UnityEngine.Rendering.CullMode)]
        _CullMode1 ("Cull", Float) = 0
        _SRef ("Stencil Refrence", Float) = 1
        [Enum(UnityEngine.Rendering.CompareFunction)]
            _SComp ("Stencil Comparison", Float) = 8
        [Enum(UnityEngine.Rendering.StencilOp)]
            _SOp("StencilOperation", Float) = 2
        [Emum(UnityEngine.Rendering.ColorWriteMask)] _ColorMask("Color Mask", Int) = 0
        [Enume(Off,0,On,1)] _ZWrite("ZWrite", Int) = 0

    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue" = "Geometry" }
        ColorMask [_ColorMask]
        ZWrite [_ZWrite]
        Cull [_CullMode1]
        LOD 100

        Pass
        {
            Stencil
            {
                Ref [_SRef]
                Comp [_SComp]
                Pass [_SOp]
            }
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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                return col;
            }
            ENDCG
        }
    }
}
