Shader "Custom/Vertex Offset"
{
    Properties // input data
    {
        //_MainTex ("Texture", 2D) = "white" {}
        _ColorA ("Color A", Color) = (1, 1, 1, 1)
        _ColorB ("Color B", Color) = (1, 1, 1, 1)
        _ColorStart ("Color Start", Range(0, 1)) = 0
        _ColorEnd ("Color End", Range(0, 1)) = 1
    }
    SubShader
    {
        Tags
        {
            "RenderType"="Opaque" // tag to inform render pipeline what type this is, for post processing
        }

        Pass
        {
            // pass tags
            // Blend DstColor Zero // multiply

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            #define TAU 6.28318530718
            
            float4 _ColorA;
            float4 _ColorB;
            float _ColorStart;
            float _ColorEnd;

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

                float wave = cos((v.uv0.y - _Time.y * 0.1) * TAU * 5);

                v.vertex.y = wave;

                o.vertex = UnityObjectToClipPos(v.vertex); // converts local space to clip space
                o.normal = UnityObjectToWorldNormal(v.normals); // just pass through
                o.uv = v.uv0; //(v.uv0 + _Offset) * _Scale; // pass through

                return o;
            }

            float InverseLerp(float startValue, float endValue, float inputValue)
            {
                return (inputValue - startValue) / (endValue - startValue);
            } 

            float4 frag (Interpolators i) : SV_Target
            {
                float wave = cos((i.uv.y - _Time.y * 0.1) * TAU * 5) * 0.5 + 0.5;
                return wave;
            }

            ENDCG
        }
    }
}
