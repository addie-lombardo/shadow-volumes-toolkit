// Shadow Volumes Toolkit
// Copyright 2012 Gustav Olsson
Shader "Shadow Volumes/Screen Interpolate"
{
	SubShader
	{
		Tags
		{
			"Queue" = "Overlay+511"
			"IgnoreProjector" = "True"
		}
		
		// Linearly interpolate the scene color towoards the shadow color
		Pass
		{
			Lighting Off
			Cull Off
			ZTest Always
			ZWrite Off
			Blend DstAlpha OneMinusDstAlpha, Zero One
			// Legacy:
			// Blend DstAlpha OneMinusDstAlpha
			// ColorMask RGB
			
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