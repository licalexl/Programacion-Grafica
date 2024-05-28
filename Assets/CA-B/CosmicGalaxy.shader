Shader "Jettelly/CosmicGalaxy"
{
    Properties
    {
        _MainColor("Main Color", Color) = (0.0, 0.0, 0.0, 1.0) // Color principal del espacio
        _StarColor("Star Color", Color) = (1.0, 1.0, 1.0, 1.0) // Color de las estrellas
        _NebulaColor("Nebula Color", Color) = (0.5, 0.0, 0.5, 1.0) // Color de las nebulosas
        _StarDensity("Star Density", Range(0.0, 1.0)) = 0.5 // Densidad de las estrellas
        _NebulaDensity("Nebula Density", Range(0.0, 1.0)) = 0.5 // Densidad de las nebulosas
        _NoiseScale("Noise Scale", Range(0.1, 10.0)) = 1.0 // Escala del ruido para nebulosas
        _WaveSpeed("Wave Speed", Range(0.1, 10.0)) = 1.0 // Velocidad de las ondas
        _ColorSpeed("Color Speed", Range(0.1, 10.0)) = 1.0 // Velocidad de cambio de color
    }
        SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 100

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

            float4 _MainColor;
            float4 _StarColor;
            float4 _NebulaColor;
            float _StarDensity;
            float _NebulaDensity;
            float _NoiseScale;
            float _WaveSpeed;
            float _ColorSpeed;

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

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float time = _Time.y * _WaveSpeed;
                float2 uv = i.uv * _NoiseScale;

                // Añadir movimiento ondulatorio
                uv.x += sin(uv.y * 10.0 + time) * 0.1;
                uv.y += cos(uv.x * 10.0 + time) * 0.1;

                // Generar estrellas
                float starValue = noise(uv * 100.0) * _StarDensity;
                float4 starColor = step(0.95, starValue) * _StarColor;

                // Generar nebulosas
                float nebulaValue = perlin(uv) * _NebulaDensity;
                float4 nebulaColor = nebulaValue * _NebulaColor;

                // Oscilar color entre morado y negro
                float colorOscillation = (sin(_Time.y * _ColorSpeed) + 1.0) * 0.5;
                float4 dynamicNebulaColor = lerp(float4(0, 0, 0, 1), _NebulaColor, colorOscillation);

                // Color final del espacio
                float4 finalColor = _MainColor + starColor + dynamicNebulaColor;
                return finalColor;
            }
            ENDCG
        }
    }
        FallBack "Diffuse"
}