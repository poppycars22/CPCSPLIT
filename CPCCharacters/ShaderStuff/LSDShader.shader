
    Shader "ShaderMan/LSDShaderTest"
	{
	Properties{
	_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
	Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
	Pass
	{

	ZWrite Off
	Blend SrcAlpha OneMinusSrcAlpha
	CGPROGRAM
	#pragma vertex vert
	#pragma fragment frag
	#include "UnityCG.cginc"
			
    
	sampler2D _MainTex;
    float4 vec4(float x,float y,float z,float w){return float4(x,y,z,w);}
    float4 vec4(float x){return float4(x,x,x,x);}
    float4 vec4(float2 x,float2 y){return float4(float2(x.x,x.y),float2(y.x,y.y));}
    float4 vec4(float3 x,float y){return float4(float3(x.x,x.y,x.z),y);}


    float3 vec3(float x,float y,float z){return float3(x,y,z);}
    float3 vec3(float x){return float3(x,x,x);}
    float3 vec3(float2 x,float y){return float3(float2(x.x,x.y),y);}

    float2 vec2(float x,float y){return float2(x,y);}
    float2 vec2(float x){return float2(x,x);}

    float vec(float x){return float(x);}
    
            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

	struct VertexInput {
    float4 vertex : POSITION;
	float2 uv:TEXCOORD0;
    float4 tangent : TANGENT;
    float3 normal : NORMAL;
	//VertexInput
	};
	struct VertexOutput {
	float4 pos : SV_POSITION;
	float2 uv:TEXCOORD0;
	//VertexOutput
	};
	
	
	VertexOutput vert (VertexInput v)
	{
	VertexOutput o;
	o.pos = UnityObjectToClipPos (v.vertex);
	o.uv = v.uv;
	//VertexFactory
	return o;
	}
    
    



#define pi 3.14159265359

uniform float2 u_resolution;
uniform float u_time;

float3 hsb2rgb( in float3 c){
 float3 rgb = clamp(abs(fmod(c.x*6.0+vec3(0.0,4.0,2.0), 6.0)-3.0)-1.0, 0.0, 1.0 );
 rgb = rgb*rgb*(3.0-2.0*rgb);  return c.z * lerp(vec3(1.0), rgb, c.y);
}

float3 rect(float2 uv, float2 c, float2 s, float2 off){
  float p = max(smoothstep(c.x+s.x,c.x+s.x+off.x, uv.x),
                smoothstep(c.y+s.y,c.y+s.y+off.y,uv.y));
  float q = max(smoothstep(c.x-s.x,c.x-s.x-off.x, uv.x),
                smoothstep(c.y-s.y,c.y-s.y-off.y,uv.y));
  return vec3(1.-max(p,q));
}

float map(float x, float a1, float a2, float b1, float b2){
  return b1 + (b2-b1) * (x-a1) / (a2-a1);
}

float3 ellipse(float2 uv, float2 c, float r){
  float d = distance(uv,c);
  return vec3(1.-smoothstep(r, r+0.08, d));
}

float3 shape(float2 st, int N, float scl, float smth, float rot){
  // Remap the space to -1. to 1.
  st = st *2.-1.;
  // Angle and radius from the current pixel
  float a = atan2(st.y,st.x)+pi+u_time*rot;
  float r = pi*2./float(N);
  // Shaping function that fmodulate the distance
  float d = cos(floor(.5+a/r)*r-a)*length(st*2.)/scl;
  return vec3(1.0-smoothstep(r,r+smth,d));
}

float maxrect(float2 uv, float2 c){
	return max(abs(c.x-uv.x), abs(c.y-uv.y));
}

float minrect(float2 uv, float2 c){
	return min(abs(c.x-uv.x), abs(c.y-uv.y));
}


float dist(float2 uv, float2 c){
	return distance(uv,c);
}





    
    
	fixed4 frag(VertexOutput vertex_output, v2f i) : SV_Target
	{
	
   float t = _Time.y;
   float2 uv = vertex_output.uv / 1;
   float2 c = vec2(.5);
   float d0 = dist(uv, c);
   float d1 = maxrect(uv, c);
   float d2 = minrect(uv, c);
   float3 color = tex2D(_MainTex, i.uv);
   float v = .5+.5*sin(d2/d1/d0*.8+(t*1.5));
   color.r = map(d0/2.+t/30., 0., 0.5, .0, 1.);
   color.g = 1.-d0*.5;
   color.b = .6-d0*2.+v;


   return vec4(hsb2rgb(color),1.);
 
	}
	ENDCG
	}
  }
  }
