// Shadow Volumes Toolkit
// Copyright 2012 Gustav Olsson
Shader "Shadow Volumes/Shadow Volume Back NoBlendOp"
{
	SubShader
	{
		Tags
		{
			"Queue" = "Overlay+507"
			"IgnoreProjector" = "True"
		}
		
		Pass
		{
			Lighting Off
			Cull Front
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