Shader "CPC/EnchantmentGlintShader"
{
    Properties
    {
        _MainTex ("Main Texture (Sword)", 2D) = "white" {}
        _GlintTex ("Glint Texture", 2D) = "white" {}
        // We have used 'Vector' because Unity ShaderLab does not support Float2
        _GlintSpeed ("Glint Speed (XY)", Vector) = (0.07, 0.07, 0, 0)
        _GlintSize  ("Glint Size (XY)", Vector) = (0.03, 0.03, 0, 0)

        _GlintColor ("Glint Color", Color) = (1,1,1,1)
        _GlintIntensity ("Glint Intensity", Range(0, 8)) = 2
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100

        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite On
        Cull Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv     : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4    _MainTex_ST;

            sampler2D _GlintTex;
            float4    _GlintTex_ST;

            float2    _GlintSpeed;
            float4    _GlintColor;
            float2    _GlintSize;
            float     _GlintIntensity;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);

                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

                float time = _Time.y;
                float glintX = frac(i.uv.x * _GlintSize.x + _GlintSpeed.x * time);
                float glintY = frac(i.uv.y * _GlintSize.y + _GlintSpeed.y * time);
                float2 glintUV = float2(glintX, glintY);

                glintUV = TRANSFORM_TEX(glintUV, _GlintTex);

                fixed4 glint = tex2D(_GlintTex, glintUV) * (_GlintColor * _GlintIntensity);

                col.rgb += glint.rgb * glint.a * 0.3;

                return col;
            }
            ENDCG
        }
    }
}
