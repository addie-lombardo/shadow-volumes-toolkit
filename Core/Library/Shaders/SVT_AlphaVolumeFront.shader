// Shadow Volumes Toolkit
// Copyright 2013 Gustav Olsson
Shader "Shadow Volumes/Alpha Volume Front"
{
	SubShader
	{
		Tags
		{
			"Queue" = "AlphaTest+505"
			"IgnoreProjector" = "True"
		}
		
		Pass
		{
			Lighting Off
			Cull Back
			ZTest LEqual
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