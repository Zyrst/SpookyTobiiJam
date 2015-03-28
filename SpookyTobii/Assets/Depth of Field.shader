// Upgrade NOTE: replaced 'texRECT' with 'tex2D'

// Upgrade NOTE: replaced 'samplerRECT' with 'sampler2D'

Shader "Custom/Depth of Field" {
	Properties {
		//_Color ("Color", Color) = (1,1,1,1)
		//_MainTex ("Albedo (RGB)", 2D) = "white" {}
		//_Glossiness ("Smoothness", Range(0,1)) = 0.5
		//_Metallic ("Metallic", Range(0,1)) = 0.0
		_MainTex("",RECT) = "white"{}
		_BlurTex1("",RECT) = "white"{}
		_BlurTex2("",RECT) = "white"{}
		_DepthText("",RECT) = "white"{}
		
	}
	SubShader {
		ZTest Always Cull Off Fog {Mode off}
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0
		#include "UnityCG.cginc"
		uniform sampler2D _MainTex 		: register(s0);
		uniform sampler2D _BlurTex1 	: register(s1);
		uniform sampler2D _BlurTex2		: register(s2);
		uniform sampler2D _DepthText	: register(s3);
		
		uniform float4 _focalPoints;
		
		struct v2f{
		float2 uv[4] : TEXCOORD0;
		};
		
		
		float DOFFactor(float z){
			float focalDist = _focalPoints.x;
			float invRange = _focalPoints.w;
			
			float fromFocal = z - focalDist;
			if(fromFocal < 0.0)
				fromFocal *= 4.0;
			return saturate( abs( fromFocal) * invRange);
		}
		uniform float4 _MainTex_TexelSize;
		
		half4 frag (v2f i) : Color
		{
			if(_MainTex_TexelSize.y < 1){
				i.uv[0].y = 1-i.uv[0].y;
				i.uv[1].y = 1-i.uv[1].y;
				i.uv[2].y = 1-i-uv[2].y;
			}
			i.uv[0].xy -= _MainTex_TexelSize.yy;
			i.uv[3].xy -= _MainTex_TexelSize.yy;
			
			half4 original = tex2D(_MainTex, i.uv[0]);
			half3 blur1 = tex2D(_BlurTex1, i.uv[1]).rgb;
			half3 blur2 = tex2D(_BlurTex2, i.uv[2]).rgb;
			
			float dof = tex2D(_DepthTex, i.uv[3]).r;
			
			half dof2;
			if(dof > 0.5)
				dof2 = saturate( dof * 0.25 + 0.75);
			else
				dof2 = saturate(dof * 1.5);
			half factor = saturate( dof*1.5 - 0.75);
			half3 blur = lerp(blur1, blur2,factor);
			half3 col = lerp(original.rgb, blur, dof);
			return float4(col, original.rgb);
		}
		} 
		ENDCG
	} 
	FallBack off
}
