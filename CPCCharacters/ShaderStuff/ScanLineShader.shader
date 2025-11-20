Shader "AALUND13/ScanLineShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ScanlineIntensity ("Scanline Intensity", Range(0, 1)) = 0.5
        _ScanlineFrequency ("Scanline Frequency", Float) = 100.0
        _ScanlineThickness ("Scanline Thickness", Range(0, 1)) = 0.1
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Pass
        {
            ZTest Always
            ZWrite Off
            Cull Off

            HLSLPROGRAM
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

            sampler2D _MainTex;
            float4 _MainTex_TexelSize; // Contains 1/width, 1/height, width, height

            float _ScanlineIntensity;
            float _ScanlineFrequency;
            float _ScanlineThickness;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // Sample the base texture
                fixed4 col = tex2D(_MainTex, i.uv);

                // Calculate scanline effect
                float scanline = frac(i.uv.y * _ScanlineFrequency); // Creates repeating pattern
                scanline = smoothstep(0.0, _ScanlineThickness, scanline); // Smooth threshold for the scanline

                // Blend the scanline with the texture
                col.rgb *= lerp(1.0, _ScanlineIntensity, scanline);

                return col;
            }
            ENDHLSL
        }
    }

    FallBack "Diffuse"
}
