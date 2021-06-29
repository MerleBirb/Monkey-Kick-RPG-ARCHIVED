Shader "Custom/Shader1"
{
    Properties // input data
    {
        //_MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1, 1, 1, 1)
        _Scale ("UV Scale", Float) = 1
        _Offset ("UV Offset", Float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            
            float4 _Color;
            float _Scale;
            float _Offset;

            // automatically filled out by Unity
            struct MeshData // per-vertex mesh data
            {
                float4 vertex : POSITION; // vertex position
                float3 normals : NORMAL;
                //float4 tangent: TANGENT;
                //float4 color: COLOR;
                float4 uv0 : TEXCOORD0; // uv0 coordinates
                //float2 uv1 : TEXCOORD1; // uv1 coordinates
            };

            struct Interpolators
            {
                float4 vertex : SV_POSITION; // clip space position
                float3 normal : TEXCOORD0;
                float2 uv : TEXCOORD1;    
            };

            Interpolators vert (MeshData v)
            {
                Interpolators o;
                o.vertex = UnityObjectToClipPos(v.vertex); // converts local space to clip space
                o.normal = UnityObjectToWorldNormal(v.normals); // just pass through
                o.uv = v.uv0 * _Scale; // pass through

                return o;
            }

            // bool is more 0 and 1 than true or false
            // int
            // float (32 bit float)
            // half (16 bit float)
            // fixed (12 bit float, lower precision, -1 to 1)
            // float4x4 = float matrix (same with half4x4, fixed4x4)

            float4 frag (Interpolators i) : SV_Target
            {
                return float4(i.uv, 0, 1);
            }

            ENDCG
        }
    }
}
