// Shadow Volumes Toolkit
// Copyright 2012 Gustav Olsson
Shader "Shadow Volumes/Shadow Volume Back"
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
			BlendOp RevSub
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