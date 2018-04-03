// Shadow Volumes Toolkit
// Copyright 2013 Gustav Olsson
Shader "Shadow Volumes/Stencil Volume Back Front Always"
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
				CompBack Always
				PassBack IncrWrap
				CompFront Always
				PassFront DecrWrap
			}
			
			Lighting Off
			Cull Off
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