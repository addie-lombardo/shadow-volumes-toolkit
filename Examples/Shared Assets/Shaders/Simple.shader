Shader "Shadow Volumes/Simple"
{
	Properties
	{
		_color ("Diffuse Color", Color) = (1, 1, 1, 1)
	}
	
	SubShader
	{
		Tags { "RenderType" = "Opaque" }
		
		CGPROGRAM
		#pragma surface surf Lambert
		#include "UnityCG.cginc"
		
		fixed4 _color;
		
		struct Input
		{
			float3 worldPos;
		};
		
		void surf(Input IN, inout SurfaceOutput o)
		{
			o.Albedo = _color.xyz;
			o.Alpha = 1.0f;
		}
		ENDCG
	}
	
	FallBack "Diffuse"
}