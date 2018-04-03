// Shadow Volumes Toolkit
// Copyright 2012 Gustav Olsson
Shader "Shadow Volumes/Screen Clear"
{
	SubShader
	{
		Tags
		{
			"Queue" = "Overlay+500"
			"IgnoreProjector" = "True"
		}
		
		Pass
		{
			Lighting Off
			Cull Off
			ZTest Always
			ZWrite Off
			Blend Zero One, Zero Zero
			// Legacy:
			// Blend Zero Zero
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