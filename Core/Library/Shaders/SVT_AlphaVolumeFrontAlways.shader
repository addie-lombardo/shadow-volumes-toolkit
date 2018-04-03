// Shadow Volumes Toolkit
// Copyright 2013 Gustav Olsson
Shader "Shadow Volumes/Alpha Volume Front Always"
{
	SubShader
	{
		Tags
		{
			"Queue" = "AlphaTest+503"
			"IgnoreProjector" = "True"
		}
		
		Pass
		{
			Lighting Off
			Cull Back
			ZTest Always
			ZWrite Off
			Blend Zero One, One One
			BlendOp RevSub
			
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