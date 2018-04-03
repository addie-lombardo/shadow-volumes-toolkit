// Shadow Volumes Toolkit
// Copyright 2012 Gustav Olsson
Shader "Shadow Volumes/Shadow Volume Front Always NoBlendOp"
{
	SubShader
	{
		Tags
		{
			"Queue" = "Overlay+503"
			"IgnoreProjector" = "True"
		}
		
		Pass
		{
			Lighting Off
			Cull Back
			ZTest Always
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