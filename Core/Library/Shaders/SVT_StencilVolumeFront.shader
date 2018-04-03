// Shadow Volumes Toolkit
// Copyright 2013 Gustav Olsson
Shader "Shadow Volumes/Stencil Volume Front"
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
			Stencil
			{
				Comp Always
				Pass IncrSat
			}
			
			Lighting Off
			Cull Back
			ZTest LEqual
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