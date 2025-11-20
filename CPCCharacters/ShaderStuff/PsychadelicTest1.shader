Shader "Custom/TrippyPostProcessing"
{
    Properties
    {
        _WaveSpeed("Wave Speed", Float) = 1.0
        _WaveStrength("Wave Strength", Float) = 0.1
        _ColorSpeed("Color Speed", Float) = 1.0
        _MainTex("Screen Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "Queue" = "Overlay" "RenderType" = "Transparent" }
        Pass
        {
            ZWrite Off
            Cull Off
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float _WaveSpeed;
            float _WaveStrength;
            float _ColorSpeed;

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // Sample the screen texture
                float4 screenColor = tex2D(_MainTex, i.uv);

                // Apply wavy distortion to UV coordinates
                float wave = sin(_Time.y * _WaveSpeed + i.uv.y * 10.0) * _WaveStrength;
                float2 distortedUV = i.uv + float2(wave, 0.0);

                // Sample the screen texture again with distorted UVs
                float4 distortedColor = tex2D(_MainTex, distortedUV);

                // Apply a psychedelic color effect
                float3 psychedelicColor = float3(
                    sin(_Time.y * _ColorSpeed + distortedUV.x * 10.0),
                    sin(_Time.y * _ColorSpeed + distortedUV.y * 10.0 + 2.0),
                    sin(_Time.y * _ColorSpeed + (distortedUV.x + distortedUV.y) * 5.0)
                );

                // Normalize the color range to 0-1
                psychedelicColor = 0.5 + 0.5 * psychedelicColor;

                // Combine the distorted color with the psychedelic overlay
                float3 finalColor = distortedColor.rgb * psychedelicColor;

                // Output the final color
                return float4(finalColor, screenColor.a);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
