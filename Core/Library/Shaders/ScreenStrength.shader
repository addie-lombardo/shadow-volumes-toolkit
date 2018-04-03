// Shadow Volumes Toolkit
// Copyright 2012 Gustav Olsson
Shader "Shadow Volumes/Screen Strength"
{
	SubShader
	{
		Tags
		{
			"Queue" = "Overlay+510"
			"IgnoreProjector" = "True"
		}
		
		// Set the shadow strength
		Pass
		{
			Lighting Off
			Cull Off
			ZTest Always
			ZWrite Off
			Blend Zero One, DstAlpha Zero
			// Legacy:
			// Blend DstAlpha Zero
			// ColorMask A
			
			Fog
			{
				Mode Off
			}
			
			CGPROGRAM
			#pragma vertex ScreenVertex
			#pragma fragment ScreenColorFragment
			#include "../Includes/Screen.cginc"
			ENDCG
		}
	}
}