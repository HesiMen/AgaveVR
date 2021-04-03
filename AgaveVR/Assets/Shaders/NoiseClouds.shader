Shader "Custom/NoiseClouds"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _SecondaryTex("Secondary Texture", 2D) = "white" {}
        _Mask("Mask", 2D) = "white" {}

        _PrimaryColor("Primary Color", Color) = (1,1,1,1)
        [HDR] _AmbientColor("Ambient Color", Color) = (1, 1, 1, 1)
        //_DarkSideStrength("Dark Side Strength", Range(0,1)) = 0.5

        [Header(Visual Parameters)]
        _ScrollSpeedDamper ("Scroll Speed Damper", Float) = 1
        _FluffFactor ("Fluffiness", Range(-1, .4)) = .125
        _Coverage ("Coverage", Range(-1,.5)) = .25

        //[Header(Displacement)]
        //_DisplacementSpeed ("Displacement Speed", Range(0,1)) = 0
        //_DisplacementScale ("Displacement Scale", Range(0,1)) = 0
        //_Frequency ("Frequency", Float) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue" = "Transparent" }
        LOD 100
        Blend SrcAlpha OneMinusSrcAlpha

        Cull Off
        ZWrite Off

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
                float2 uv3: TEXCOORD2;
                float3 normal : NORMAL;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 worldNormal: NORMAL;
                float3 viewDir : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float2 uv3 : TEXCOORD3;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                UNITY_VERTEX_OUTPUT_STEREO
            };

            sampler2D _MainTex;
            sampler2D _SecondaryTex;
            sampler2D _Mask;
            float4 _SecondaryTex_ST;
            float4 _MainTex_ST;
            float4 _Mask_ST;

            fixed _ScrollSpeedDamper;
            half _FluffFactor;
            half _Coverage;

            //half _DisplacementSpeed;
            //half _DisplacementScale;
            //half _Frequency;
            fixed4 _PrimaryColor;
            fixed4 _AmbientColor;
            //fixed _DarkSideStrength;

            v2f vert (appdata v)
            {
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_TRANSFER_INSTANCE_ID(v, o);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

                o.viewDir = WorldSpaceViewDir(v.vertex);
                //v.vertex.xyz += _DisplacementScale * sin(_Time.y * _DisplacementSpeed + v.vertex.y * _Frequency); 
                o.vertex = UnityObjectToClipPos(v.vertex);
                v.uv.y += _Time.y / _ScrollSpeedDamper;
                v.uv2.xy += _Time.y / (_ScrollSpeedDamper / 3);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.uv2 = TRANSFORM_TEX(v.uv2, _SecondaryTex);
                o.uv3 = TRANSFORM_TEX(v.uv3, _Mask);

                o.worldNormal = UnityObjectToWorldNormal(v.normal);

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                UNITY_SETUP_INSTANCE_ID(i);
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);

                fixed3 normal = normalize(i.worldNormal);
                fixed NdotL = dot(_WorldSpaceLightPos0, normal);
                fixed lightIntensity = NdotL * 0.5 + 0.75;
                //lightIntensity = smoothstep(-.5, 1, NdotL);
                fixed4 light = lightIntensity * (_LightColor0);

                //half3 viewDir = normalize(i.viewDir);

                // sample the texture
                //float4 sample = tex2D(_MainTex, i.uv);
                fixed4 col = tex2D(_MainTex, i.uv) * tex2D(_SecondaryTex,i.uv2);
                col = ((col < _FluffFactor) ? 0 : smoothstep(_Coverage, 1, col));
                col.rgb = 1 - col.rgb;
                col *= tex2D(_Mask, i.uv3);
                //col *= 2;
                //col = smoothstep(0, 1, col);
                col *= (_AmbientColor + light) * _PrimaryColor;
                //col.a = 0;
                return col;

                /*fixed4 col = tex2D(_MainTex, i.uv);
                return col;*/
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
