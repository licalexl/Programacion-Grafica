Shader "Jettelly/Ice"
{
    Properties
    {
        _IceColor("Ice Color", Color) = (0.6, 0.9, 1.0, 1.0) // Color del hielo
        _FresnelPower("Fresnel Power", Range(0.1, 5.0)) = 2.0 // Potencia del efecto Fresnel
        _DistortionScale("Distortion Scale", Range(0.0, 1.0)) = 0.1 // Escala de la distorsión
        _SpecularColor("Specular Color", Color) = (1.0, 1.0, 1.0, 1.0) // Color especular
        _Shininess("Shininess", Range(0.03, 1.0)) = 0.5 // Brillo del hielo
        _NoiseTex("Noise Texture", 2D) = "white" {} // Textura de ruido
        _CrackTex("Crack Texture", 2D) = "white" {} // Textura de grietas
        _NoiseSpeed("Noise Speed", Range(0.1, 5.0)) = 1.0 // Velocidad del ruido
        _CrackIntensity("Crack Intensity", Range(0.0, 1.0)) = 0.5 // Intensidad de las grietas
    }
        SubShader
        {
            Tags { "RenderType" = "Opaque" }
            LOD 300

            CGPROGRAM
            #pragma surface surf Standard fullforwardshadows

            sampler2D _NoiseTex;
            sampler2D _CrackTex;
            half _FresnelPower;
            float4 _IceColor;
            float4 _SpecularColor;
            float _Shininess;
            half _DistortionScale;
            half _NoiseSpeed;
            half _CrackIntensity;

            struct Input
            {
                float2 uv_NoiseTex;
                float3 worldRefl; // Vector de reflexión
                INTERNAL_DATA
            };

            // Función Fresnel para simular la reflexión del hielo
            half4 LightingStandard(SurfaceOutputStandard s, half3 viewDir, UnityGI gi)
            {
                half3 worldNormal = normalize(s.Normal);
                half fresnel = pow(1.0 - saturate(dot(viewDir, worldNormal)), _FresnelPower);

                half4 c = s.Albedo * _IceColor;
                c.rgb += _SpecularColor.rgb * s.Gloss * fresnel;

                UnityGIInput data = UnityGIInput(s, viewDir, worldNormal);
                half4 result = UnityGlobalIllumination(data, s);
                result.rgb += s.Emission.rgb;

                return result;
            }

            void surf(Input IN, inout SurfaceOutputStandard o)
            {
                float time = _Time.y * _NoiseSpeed;
                float2 uv = IN.uv_NoiseTex;

                // Añadir distorsión usando la textura de ruido
                float2 noiseUV = uv + float2(sin(time), cos(time)) * _DistortionScale;
                float noiseValue = tex2D(_NoiseTex, noiseUV).r;

                // Añadir textura de grietas
                float crackValue = tex2D(_CrackTex, uv).r * _CrackIntensity;

                // Color base del hielo con distorsión y grietas
                o.Albedo = _IceColor.rgb * (1.0 - noiseValue) * (1.0 - crackValue);

                // Efecto Fresnel para el hielo
                float3 worldNormal = normalize(IN.worldRefl);
                float fresnel = pow(1.0 - saturate(dot(worldNormal, normalize(WorldSpaceViewDir(IN.worldPos)))), _FresnelPower);

                o.Emission = _IceColor.rgb * fresnel;
                o.Specular = _SpecularColor.rgb;
                o.Smoothness = _Shininess;
            }
            ENDCG
        }
            FallBack "Diffuse"
}
