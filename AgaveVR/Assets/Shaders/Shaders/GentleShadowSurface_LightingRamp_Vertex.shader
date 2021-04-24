Shader "Custom/GentleShadowSurface/LightingRamp-Vertex"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _LightRamp("LightRamp", 2D) = "white"{}
        _NormalMap("Normal" , 2D) = "bump" {}
        _NormalStrength("Normal Strength", Range(0,1)) = 1
        _LightRampStrength("Ramp Strength", Range(0, 10)) = 1
        
        [Header(Noise)]
        _NoiseTex("Noise", 2D) = "white" {}
        _DisplacementScale("Displacement Scale", Float) = 0
        _Frequency("Frequency", Float) = 0
        _ScrollSpeed("Scroll Speed Damper", Range(0,1)) = 0
        _WindStrength("Wind Strength", Float) = 0
        _DispCutoff("Displacement Cutoff", Range(-1,1)) = 0


        //Dissolve properties
        [Header(Dissolve)]
		_DissolveTexture("Dissolve Texutre", 2D) = "white" {} 
		_Amount("Amount", Range(0,1)) = 0

    
        [Enum(UnityEngine.Rendering.CullMode)]
        _CullModel("Cull", Float) = 2
        [Enume(Off,0,On,1)] _ZWrite("ZWrite", Int) = 0

        [Space(10)]
        [Header(Stencil Properties)]
        _SRef ("Stencil Refrence", Float) = 1
        [Enum(UnityEngine.Rendering.CompareFunction)]
        _SComp ("Stencil Comparison", Float) = 8
        [Enum(UnityEngine.Rendering.StencilOp)]
        _SOp("StencilOperation", Float) = 2
        [Emum(UnityEngine.Rendering.ColorWriteMask)] _ColorMask("Color Mask", Int) = 14

    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200
        Cull [_CullModel]

        Stencil
        {
            Ref[_SRef]
            Comp[_SComp]
            Pass[_SOp]
        }

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf WrapLambert fullforwardshadows exclude_path:deferred vertex:vert
        #pragma target 3.0

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        half4 LightingWrapLambert (SurfaceOutput s, half3 lightDir, half atten)
        {
            half NDotL = dot (s.Normal, lightDir);
            half diff = NDotL * 0.5 + 0.75;
            half4 c;
            c.rgb = s.Albedo * (_LightColor0.rgb / 1.25) * (diff * atten);
            c.a = 1;
            return c;
        }

        sampler2D _MainTex;
        sampler2D _NormalMap;
        sampler2D _LightRamp;
        sampler2D _NoiseTex;
        float4 _NoiseTex_ST;

        struct Input
        {
            float2 uv_MainTex: TEXCOORD0;
            float2 uv_NormalMap: TEXCOORD1;
            float2 uv_NoiseMap: TEXCOORD2;
            float3 viewDir;
            float3 worldPos;
            float3 vertexCoord;
            float3 worldNormal;
            
        };

        fixed4 _Color;
        fixed _NormalStrength;
        half _LightRampStrength;


        //Dissolve properties
		sampler2D _DissolveTexture;
		half _Amount;
        half _DisplacementScale;
        half _Frequency;
        half _ScrollSpeed;
        float _WindStrength;
        float _DispCutoff;



        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void vert (inout appdata_full v, out Input o)
        {
            UNITY_INITIALIZE_OUTPUT(Input, o);
            //float4 anchorTex = tex2Dlod(_AnchorMap, float4 (v.texcoord3.xy,0,0));
            float3 gWorldPos = UnityObjectToWorldNormal(v.vertex);
            float2 noiseTex = tex2Dlod(_NoiseTex, float4(gWorldPos.xz * _NoiseTex_ST.xy + _Time.y * _ScrollSpeed * (v.texcoord.y * _DispCutoff), 0.0, 0.0)).xy;

            //o.viewDir = WorldSpaceViewDir(v.vertex);
            //v.texcoord.x += _Time.y / (_ScrollSpeed / 600);
            //v.texcoord2.x += _Time.y / _ScrollSpeed;

            //Bounce Grass
            //v.vertex.xyz += (_DisplacementScale * (sin((_Time.y * _ScrollSpeed) + UnityObjectToWorldNormal(v.vertex) * _Frequency)) * v.texcoord.y);

            float2 wind = (noiseTex * 2.0 - 1.0) * _WindStrength;
            v.vertex.xy += wind;

            o.vertexCoord = UnityObjectToClipPos(v.vertex);
            //o.uv = TRANSFORM_TEX (v.uv, _MainTex);

            o.worldNormal = UnityObjectToWorldNormal(v.normal);
        }

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = (tex2D (_MainTex, IN.uv_MainTex) * _Color) * (tex2D(_LightRamp, IN.uv_MainTex) * _LightRampStrength);
            o.Albedo = c.rgb;
            half3 normal = UnpackNormal(tex2D(_NormalMap, IN.uv_NormalMap));
            normal.z *= (1-_NormalStrength);
            o.Normal = normalize(normal);


            half dissolve_value = tex2D(_DissolveTexture, IN.uv_MainTex).r;
			clip(dissolve_value - _Amount);
 
			//Basic shader function
			//fixed4 d = tex2D (_MainTex, IN.uv_MainTex) * _Color; 
			 o.Emission = fixed3(1, 1, 1) * step( dissolve_value - _Amount, 0.005f);
 
			//o.Albedo = d.rgb;
			//o.Metallic = _Metallic;
			//o.Smoothness = _Glossiness;
			o.Alpha = c.a;



        }
        ENDCG
    }
    FallBack "Diffuse"
}
