// Shadow Volumes Toolkit
// Copyright 2012 Gustav Olsson
Shader "Shadow Volumes/Shadow Volume Front"
{
	SubShader
	{
		Tags
		{
			"Queue" = "Overlay+505"
			"IgnoreProjector" = "True"
		}
		
		Pass
		{
			Lighting Off
			Cull Back
			ZTest LEqual
			ZWrite Off
			Blend Zero One, One One
			// Legacy:
			// Blend One One
			// ColorMask A
			
			Fog
			{
				Mode Off
			}
			
			CGPROGRAM
			#pragma vertex ShadowVolumeVertex
			#pragma fragment ShadowVolumeFragment
			#include "../Includes/ShadowVolume.cginc"
			ENDCG
		}
	}
}