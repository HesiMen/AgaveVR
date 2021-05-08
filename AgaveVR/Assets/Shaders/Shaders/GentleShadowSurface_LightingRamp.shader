Shader "Custom/GentleShadowSurface/LightingRamp"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _LightRamp("LightRamp", 2D) = "white"{}
        _NormalMap("Normal" , 2D) = "bump" {}
        _NormalStrength("Normal Strength", Range(0,1)) = 1
        _LightRampStrength("Ramp Strength", Range(0, 10)) = 1

        [Header(Snow)]
        [Toggle(SNOW_ON)]
        _SnowToggle("Snow Toggle", Int) = 0
        _SnowTex("Snow Texture", 2D) = "white"{}
        _BlendSnow("Blend Amount", Range(0,1)) = 1
        
        [Header(Dissolve)]
        //Dissolve properties
		_DissolveTexture("Dissolve Texutre", 2D) = "white" {} 
		_Amount("Amount", Range(0,1)) = 0

    
        [Enum(UnityEngine.Rendering.CullMode)]
        _CullModel("Cull", Float) = 2
        [Enum(Off,0,On,1)] _ZWrite("ZWrite", Int) = 0

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
        #pragma surface surf WrapLambert fullforwardshadows exclude_path:deferred
        #pragma multi_compile_local SNOW_OFF SNOW_ON

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
        sampler2D _SnowTex;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_NormalMap;
            float3 worldPos;
        };

        fixed4 _Color;
        float _NormalStrength;
        half _LightRampStrength;
        half _BlendSnow;
        int _SnowToggle;


        //Dissolve properties
		sampler2D _DissolveTexture;
		half _Amount;



        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c;
            fixed4 mainTex = tex2D(_MainTex, IN.uv_MainTex);
            c = mainTex * _Color;
            #if SNOW_ON
                fixed4 snowTex = tex2D(_SnowTex, IN.uv_MainTex);
                c = lerp(mainTex, snowTex, saturate((_BlendSnow - tex2D(_DissolveTexture, IN.uv_MainTex).r)));
            #endif

            c *= (tex2D(_LightRamp, IN.uv_MainTex) * _LightRampStrength);
            o.Albedo = c.rgb;

            // Normals
            float3 normal = UnpackNormal(tex2D(_NormalMap, IN.uv_NormalMap));
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
