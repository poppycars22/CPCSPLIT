Shader "Custom/AccessibilityWaveyShader"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
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
                o.uv = v.uv;

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
                float4 finalColor = float4(texColor.rgb * psychedelicColor * 0.05, texColor.a);

                return finalColor;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
