Shader "Custom/Texture Shader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Rock ("Rock", 2D) = "white" {}
        _Pattern ("Pattern", 2D) = "white" {}
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

            #define TAU 6.28318530718

            struct MeshData
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Interpolators
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 worldPos : TEXCOORD1;
            };

            sampler2D _MainTex;
            sampler2D _Pattern;
            sampler2D _Rock;

            float GetWave(float2 coord)
            {
                float wave = cos((coord - _Time.y * 0.1) * TAU * 5) * 0.5 + 0.5;
                wave *= coord;
                return wave;
            }

            Interpolators vert (MeshData v)
            {
                Interpolators o;
                o.worldPos = mul(UNITY_MATRIX_M, float4(v.vertex.xyz, 1)); // object to world
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float4 frag (Interpolators i) : SV_Target
            {
                float2 topDownProjection = i.worldPos.xz;
                float4 grass = tex2D(_MainTex, topDownProjection);
                float4 rock = tex2D(_Rock, topDownProjection);
                float pattern = tex2D( _Pattern, i.uv).x;

                float4 finalColor = lerp(rock, grass, pattern);

                return finalColor;
            }
            ENDCG
        }
    }
}
