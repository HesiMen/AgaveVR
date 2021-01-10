Shader "Agave/Unlit/DistortionShader"
{
    Properties
    {
        _Distortion ("Distortion", Range(0,1)) = 0
    }
    SubShader
    {
        Tags 
        {
            //"RenderType"="Transparent" 
            "Queue"="Transparent"
            "DisableBatching" = "True"
        }

        GrabPass
        {
            "_BackgroundTex"
        }

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

            fixed _Distortion;
            sampler2D _BackgroundTex;

            v2f vert (appdata v)
            {
                v2f o;
                //o.vertex = v.vertex;
                //Billboard Shader
                float4 pos = v.vertex;
                pos = mul(UNITY_MATRIX_P,
                      UnityObjectToViewPos(float4(0,0,0,1))
                        + float4 (pos.x, pos.z, 0, 0));
                o.vertex = pos;
                //o.vertex = UnityObjectToClipPos(o.vertex);

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = float4 (1,1,1,1);
                return col;
            }
            ENDCG
        }
    }
}
