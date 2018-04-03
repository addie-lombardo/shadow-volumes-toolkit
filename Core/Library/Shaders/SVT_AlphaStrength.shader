// Shadow Volumes Toolkit
// Copyright 2013 Gustav Olsson
Shader "Shadow Volumes/Alpha Strength"
{
	SubShader
	{
		Tags
		{
			"Queue" = "AlphaTest+510"
			"IgnoreProjector" = "True"
		}
		
		// Set the shadow strength
		Pass
		{
			Lighting Off
			Cull Off
			ZTest Always
			ZWrite Off
			Blend Zero One, DstAlpha Zero
			
			Fog
			{
				Mode Off
			}
			
			CGPROGRAM
			#pragma vertex ScreenVertex
			#pragma fragment ScreenColorFragment
			#include "../Includes/Screen.cginc"
			ENDCG
		}
	}
}