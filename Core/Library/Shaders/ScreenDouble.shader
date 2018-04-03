// Shadow Volumes Toolkit
// Copyright 2012 Gustav Olsson
Shader "Shadow Volumes/Screen Double"
{
	SubShader
	{
		Pass
		{
			Name "DOUBLE"
			
			Lighting Off
			Cull Off
			ZTest Always
			ZWrite Off
			Blend Zero One, DstAlpha One
			// Legacy:
			// Blend DstAlpha One
			// ColorMask A
			
			Fog
			{
				Mode Off
			}
			
			CGPROGRAM
			#pragma vertex ScreenVertex
			#pragma fragment ScreenFragment
			#include "../Includes/Screen.cginc"
			ENDCG
		}
	}
}