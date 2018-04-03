// Shadow Volumes Toolkit
// Copyright 2013 Gustav Olsson
Shader "Shadow Volumes/Stencil Volume Back Always"
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
			Stencil
			{
				Comp Always
				Pass IncrSat
			}
			
			Lighting Off
			Cull Front
			ZTest Always
			ZWrite Off
			Blend Zero One
			
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