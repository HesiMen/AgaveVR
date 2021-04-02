Shader "Unlit/TLViewBlocker" {
Properties {
    _Color("Main Color", Color) = (0,0,0,0)
  //  _MainTex ("Albedo (RGB)", 2D) = "white" {}
    _FadeOutCenter("Fade Out Center", Vector) = (0,0,0,0)
    _FadeOutPct("Fade Out Pct", Float) = 0
}
SubShader{
	Tags { "RenderPipeline" = "UniversalRenderPipeline" "Queue"="Overlay" "RenderType"="Opaque" "IgnoreProjector"="True"}
    Blend SrcAlpha OneMinusSrcAlpha
    LOD 100
    Cull Front
    ZWrite Off
    ZTest Always
    Pass {
            HLSLPROGRAM
            // This line defines the name of the vertex shader. 
            #pragma vertex vert
            // This line defines the name of the fragment shader. 
            #pragma fragment frag

            // The Core.hlsl file contains definitions of frequently used HLSL
            // macros and functions, and also contains #include references to other
            // HLSL files (for example, Common.hlsl, SpaceTransforms.hlsl, etc.).
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct appdata_t {
                float4 vertex : POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };
            struct v2f {
                float4 vertex : SV_POSITION;
                float alpha : TEXCOORD2;
                UNITY_VERTEX_INPUT_INSTANCE_ID
	            UNITY_VERTEX_OUTPUT_STEREO
            };
        
        CBUFFER_START(UnityPerMaterial)
            float4 _FadeOutCenter;
            half4 _Color;
            float _FadeOutPct;
        CBUFFER_END

            v2f vert (appdata_t v)
            {
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_TRANSFER_INSTANCE_ID(v, o);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
                
                o.vertex = TransformObjectToHClip(v.vertex.xyz);
                float3 vertLocalPos = 0.45 * v.vertex.xyz; // TODO scale not being applied. must do manually.
                float3 dist = vertLocalPos - _FadeOutCenter.xyz;
                half radialFalloff = min(dot(dist, dist) * _FadeOutCenter.w, 1);
                o.alpha = _FadeOutPct * smoothstep(0.7, 0.9, 1 - radialFalloff);
                return o;
            }
            half4 frag (v2f i) : SV_Target
            {
                UNITY_SETUP_INSTANCE_ID(i);
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);    
                half4 col = _Color;
                col.a = i.alpha;
                return col;
            }
        ENDHLSL
    }
}
}
