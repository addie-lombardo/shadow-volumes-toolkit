// Shadow Volumes Toolkit
// Copyright 2012 Gustav Olsson
Shader "Shadow Volumes/Screen Flip"
{
	SubShader
	{
		Pass
		{
			Name "FLIP"
			
			Lighting Off
			Cull Off
			ZTest Always
			ZWrite Off
			Blend Zero One, OneMinusDstAlpha Zero
			// Legacy:
			// Blend OneMinusDstAlpha Zero
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