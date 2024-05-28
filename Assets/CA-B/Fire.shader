Shader "Jettelly/Fire"
{
    Properties
    {
        _BaseColor("Base Color", Color) = (1.0, 0.3, 0.0, 1.0) // Color base del fuego (rojo-naranja)
        _TipColor("Tip Color", Color) = (1.0, 1.0, 0.0, 1.0) // Color de la punta del fuego (amarillo)
        _NoiseScale("Noise Scale", Range(0.1, 10.0)) = 1.0 // Escala del ruido
        _Speed("Speed", Range(0.1, 5.0)) = 1.0 // Velocidad del movimiento del fuego
        _Intensity("Intensity", Range(0.1, 2.0)) = 1.0 // Intensidad del fuego
    }
        SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 200

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            float4 _BaseColor;
            float4 _TipColor;
            float _NoiseScale;
            float _Speed;
            float _Intensity;

            // Función de ruido
            float noise(float2 uv)
            {
                return frac(sin(dot(uv, float2(12.9898, 78.233))) * 43758.5453);
            }

            // Función de ruido Perlin mejorada
            float perlin(float2 uv)
            {
                float2 i = floor(uv);
                float2 f = frac(uv);

                float2 u = f * f * (3.0 - 2.0 * f);

                return lerp(lerp(noise(i + float2(0.0, 0.0)), noise(i + float2(1.0, 0.0)), u.x),
                            lerp(noise(i + float2(0.0, 1.0)), noise(i + float2(1.0, 1.0)), u.x), u.y);
            }

            // Función de ruido fractal mejorada
            float fractalNoise(float2 uv, int octaves, float persistence)
            {
                float value = 0.0;
                float amplitude = 1.0;
                float frequency = 1.0;
                float totalAmplitude = 0.0;

                for (int i = 0; i < octaves; i++)
                {
                    value += perlin(uv * frequency) * amplitude;
                    totalAmplitude += amplitude;
                    amplitude *= persistence;
                    frequency *= 2.0;
                }

                return value / totalAmplitude;
            }

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float time = _Time.y * _Speed;
                float2 uv = i.uv * _NoiseScale;

                // Añadir movimiento vertical y horizontal al ruido para simular el fuego
                uv.y += time;
                uv.x += sin(time * 0.5) * 0.5;

                // Generar ruido fractal
                float noiseValue = fractalNoise(uv, 5, 0.5);

                // Crear un gradiente de color desde la base hasta la punta del fuego
                float flame = smoothstep(0.3, 1.0, noiseValue) * _Intensity;
                float4 color = lerp(_BaseColor, _TipColor, flame);

                // Añadir distorsión para simular el calor
                float distortion = fractalNoise(uv + time * 0.1, 3, 0.5) * 0.1;
                color.rgb += distortion;

                return color;
            }
            ENDCG
        }
    }
        FallBack "Diffuse"
}