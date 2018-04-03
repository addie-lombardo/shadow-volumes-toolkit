// Shadow Volumes Toolkit
// Copyright 2013 Gustav Olsson
Shader "Shadow Volumes/Alpha Volume Back Always"
{
	SubShader
	{
		Tags
		{
			"Queue" = "AlphaTest+501"
			"IgnoreProjector" = "True"
		}
		
		Pass
		{
			Lighting Off
			Cull Front
			ZTest Always
			ZWrite Off
			Blend Zero One, One One
			
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