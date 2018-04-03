// Shadow Volumes Toolkit
// Copyright 2013 Gustav Olsson
Shader "Shadow Volumes/Stencil Volume Front Back"
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
				CompFront Always
				PassFront IncrWrap
				CompBack Always
				PassBack DecrWrap
			}
			
			Lighting Off
			Cull Off
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