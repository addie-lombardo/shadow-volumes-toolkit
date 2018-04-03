// Shadow Volumes Toolkit
// Copyright 2013 Gustav Olsson
Shader "Shadow Volumes/Stencil Volume Back"
{
	SubShader
	{
		Tags
		{
			"Queue" = "AlphaTest+507"
			"IgnoreProjector" = "True"
		}
		
		Pass
		{
			Stencil
			{
				Comp Always
				Pass DecrSat
			}
			
			Lighting Off
			Cull Front
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