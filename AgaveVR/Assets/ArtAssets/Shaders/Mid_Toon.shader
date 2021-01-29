Shader "Custom/Lighting Model/Mid_ToonShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _PrimaryColor("Primary Color", Color) = (1,1,1,1)
        [HDR] _AmbientColor("Ambient Color", Color) = (0.4, 0.4, 0.4, 1)
        _DarkSideStrength("Dark Side Strength", Range(0,1)) = 0.5

        //Rim
        [HDR] _RimColor("Rim Color", Color) = (1,1,1,1)
        _RimAmount ("Rim Amoutn", Range(0,1)) = 0.716
        _RimThreshold ("Rim Threshold", Range(0,1)) = 0.1
    }
    SubShader
    {
        Tags 
        {
            "RenderType"="Opaque" 
            "LightMode" = "ForwardBase"
            "PassFlags" = "OnlyDirectional"
        }
        LOD 100

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
                float3 normal: NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 worldNormal: NORMAL;
                float3 viewDir : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST; 
            fixed4 _PrimaryColor;
            fixed4 _AmbientColor;
            fixed _DarkSideStrength;

            fixed4 _RimColor;
            fixed _RimAmount;
            fixed _RimThreshold;


            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.worldNormal = UnityObjectToWorldNormal(v.normal);

                //Specularity
                o.viewDir = WorldSpaceViewDir(v.vertex);

                return o;
            }

            fixed4 frag (v2f i) : SV_Target 
            {
                fixed3 normal = normalize(i.worldNormal);
                fixed NdotL = dot(_WorldSpaceLightPos0, normal);
                fixed lightIntensity = (NdotL > 0 ? 1 : 1 -  _DarkSideStrength);
                //fixed lightIntensity = smoothstep(0, 0.01, NdotL);
                fixed4 light = lightIntensity * _LightColor0;

                half3 viewDir = normalize(i.viewDir);

                // Rim Lighting
                half4 rimDot = 1 - dot(viewDir, normal);
                half rimIntensity = rimDot * pow(NdotL, _RimThreshold);
                rimIntensity = smoothstep(_RimAmount - 0.01, _RimAmount + 0.01, rimIntensity);
                fixed4 rim = rimIntensity * _RimColor;

                // sample the texture
                float4 sample = tex2D(_MainTex, i.uv);
                return (_AmbientColor + light + rim)  * _PrimaryColor * sample;
            }
            ENDCG
        }
    }
}
