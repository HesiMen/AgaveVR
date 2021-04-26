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
        
        [Header(Wind)]
        //_NoiseTex("Noise", 2D) = "white" {}
        _DisplacementScale("Displacement Scale", Range(-25, 25)) = 0
        _IndividualVFrequency("Vertex Frequency", Range(0,20)) = 0
        _BounceFrequency("Bounce Frequency", Range(0,25)) = 0
        _WindStrength("Wind Strength", Range(0,3)) = 0
        _DispCutoff("Displacement Cutoff", Range(-1,1)) = 0
        [ToolTip(Positive if off, Negative if on)]
        _XWindDirection ("_XWindDirection", Range(-1,1)) = 0
        _ZWindDirection ("_ZWindDirection", Range(-1,1)) = 0
        _WindLean ("Wind Lean", Float) = 0
        _WindFrequency("Wind Frequency", Float) = 0


        //Dissolve properties
        [Header(Dissolve)]
		_DissolveTexture("Dissolve Texutre", 2D) = "white" {} 
		_Amount("Amount", Range(0,1)) = 0

    
        [Enum(UnityEngine.Rendering.CullMode)]
        _CullModel("Cull", Float) = 2
        [Enum(Off,0,On,1)] _ZWrite("ZWrite", Int) = 0

    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200
        Cull [_CullModel]

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
        half _XWindDirection;
        half _ZWindDirection;
        half _WindLean;
        half _WindFrequency;


        //Dissolve properties
		sampler2D _DissolveTexture;
		half _Amount;
        half _DisplacementScale;
        half _IndividualVFrequency;
        half _BounceFrequency;
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
            _DisplacementScale *= .0001;
            _WindStrength *= .002;
            _IndividualVFrequency *= 400;
            _WindLean -= 1;
            _WindLean += _WindStrength;

            float commonWind = _WindLean + (1 - sin(_Time.y * _WindFrequency)) * _WindStrength;
            clamp(commonWind, 0, 1);
            float xWindPlane = (v.vertex.x + (commonWind * _XWindDirection) * v.texcoord.y);
            float zWindPlane = (v.vertex.z + (commonWind * _ZWindDirection) * v.texcoord.y);
            float yContract = v.vertex.y - (_WindLean - sin(_Time.y)) * (_WindStrength /3) * v.texcoord.y;
            float3 vertexBounce = (_DisplacementScale * (sin((_Time.y * _BounceFrequency) + (v.vertex) * _IndividualVFrequency)) * v.texcoord.y * sin(_Time.y));
            v.vertex.x = xWindPlane;
            v.vertex.z = zWindPlane;
            v.vertex.y = yContract;
            v.vertex.xyz += + vertexBounce;

            o.vertexCoord = UnityObjectToClipPos(v.vertex);
        }

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = (tex2D (_MainTex, IN.uv_MainTex) * _Color) * (tex2D(_LightRamp, IN.uv_MainTex) * _LightRampStrength);
            o.Albedo = c.rgb;
            half3 normal = UnpackNormal(tex2D(_NormalMap, IN.uv_NormalMap));
            normal.z *= (1-_NormalStrength);
            o.Normal = normalize(normal);

            //Dissolve
            half dissolve_value = tex2D(_DissolveTexture, IN.uv_MainTex).r;
			clip(dissolve_value - _Amount);
			o.Emission = fixed3(1, 1, 1) * step( dissolve_value - _Amount, 0.005f);
			o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
