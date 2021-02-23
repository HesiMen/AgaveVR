Shader "Custom/GentleShadowSurface"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _NormalMap("Normal" , 2D) = "bump" {}
    
        _FakeAmbienValue ("Fake Ambient Occlusion", Float) = 1
        [Toggle] _AmbientOcclusion ("Ambient Occlusion", Float) = 0
        _YBrightnessCutoff("Y Brightness Cutoff", Float) = 1
        _XZBrightnessCutoff ("XZ Brightness Cutoff", Float) =  1
        //_Glossiness ("Smoothness", Range(0,1)) = 0.5
        //_Metallic ("Metallic", Range(0,1)) = 0.0
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf WrapLambert fullforwardshadows

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

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_NormalMap;
            float3 worldPos;
        };

        //half _Glossiness;
       //half _Metallic;
        fixed4 _Color;
        fixed _FakeAmbienValue;
        fixed _AmbientOcclusion;
        half _YBrightnessCutoff;
        half _XZBrightnessCutoff;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutput o)
        {
            // Albedo comes from a texture tinted by color
            half yAmbientOcclusion = 
                pow((IN.worldPos.y / _FakeAmbienValue), _AmbientOcclusion) > _YBrightnessCutoff ? _YBrightnessCutoff : pow((IN.worldPos.y / _FakeAmbienValue), _AmbientOcclusion);

            half xzAmbientOcclusion = 
                (pow((abs(IN.worldPos.x) / _FakeAmbienValue), _AmbientOcclusion) > _XZBrightnessCutoff ? _XZBrightnessCutoff : pow((abs(IN.worldPos.x) / _FakeAmbienValue), _AmbientOcclusion));
                /*+
                (pow((abs(IN.worldPos.z) / _FakeAmbienValue), _AmbientOcclusion) > _XZBrightnessCutoff.y ? _XZBrightnessCutoff.y : pow((abs(IN.worldPos.z) / _FakeAmbienValue), _AmbientOcclusion));*/

            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb *
                (yAmbientOcclusion + xzAmbientOcclusion);
            o.Normal = UnpackNormal (tex2D (_NormalMap, IN.uv_NormalMap));
            // Metallic and smoothness come from slider variables
            //o.Metallic = _Metallic;
            //o.Smoothness = _Glossiness;
            //o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
