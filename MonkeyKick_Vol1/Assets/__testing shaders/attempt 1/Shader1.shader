Shader "Custom/Shader1"
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
            "RenderType"="Transparent" // tag to inform render pipeline what type this is, for post processing
            "Queue"="Transparent" // changes the render order
        }

        Pass
        {
            // pass tags

            Cull Off
            ZWrite Off
            Blend One One // additive
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
                // lerp, blend between two colors based on x uv coordinates
                // float t = saturate(InverseLerp(_ColorStart, _ColorEnd, i.uv.x));
                
                // float t = abs(frac(i.uv.x * 5) * 2 - 1);

                //return float4(i.uv, 0, 1);

                float xOffset = cos(i.uv.x * TAU * 8) * 0.01;

                float t = cos((i.uv.y + xOffset + -_Time.y * 1.2) * TAU * 5) * 0.5 + 0.5;
                t *= 1 - i.uv.y;

                float topBottomRemover = (abs(i.normal.y) < 0.999);
                float waves = t * topBottomRemover;

                float4 gradient = lerp(_ColorA, _ColorB, i.uv.y);

                return gradient * waves;
            }

            ENDCG
        }
    }
}
