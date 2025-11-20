Shader "Custom/WavyPsychedelic"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _WaveSpeed("Wave Speed", Float) = 1.0
        _WaveStrength("Wave Strength", Float) = 0.1
        _ColorSpeed("Color Speed", Float) = 1.0
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
        LOD 100

        Pass
        {
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha
            ColorMask RGBA // Ensure all color channels are written

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float _WaveSpeed;
            float _WaveStrength;
            float _ColorSpeed;

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

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);

                // Apply a wavy distortion to UV coordinates
                float wave = sin(_Time.y * _WaveSpeed + v.uv.y * 10.0) * _WaveStrength;
                o.uv = v.uv + float2(wave, 0.0); // Offset UV horizontally for the wave effect

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // Sample the main texture
                float4 texColor = tex2D(_MainTex, i.uv);

                // Create a psychedelic color effect based on time and UV
                float3 psychedelicColor = float3(
                    sin(_Time.y * _ColorSpeed + i.uv.x * 10.0),
                    sin(_Time.y * _ColorSpeed + i.uv.y * 10.0 + 2.0),
                    sin(_Time.y * _ColorSpeed + (i.uv.x + i.uv.y) * 5.0)
                );

                // Normalize the color range to 0-1
                psychedelicColor = 0.5 + 0.5 * psychedelicColor;

                // Combine the texture color with the psychedelic color
                float4 finalColor = float4(texColor.rgb * psychedelicColor, texColor.a);

                return finalColor;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
