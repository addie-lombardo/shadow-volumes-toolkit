// Shadow Volumes Toolkit
// Copyright 2013 Gustav Olsson
Shader "Shadow Volumes/Stencil Interpolate"
{
	SubShader
	{
		Tags
		{
			"Queue" = "AlphaTest+511"
			"IgnoreProjector" = "True"
		}
		
		// Linearly interpolate the scene color towoards the shadow color
		Pass
		{
			Stencil
			{
				Ref 0
				Comp NotEqual
			}
			
			Lighting Off
			Cull Off
			ZTest Always
			ZWrite Off
			Blend SrcAlpha OneMinusSrcAlpha, Zero One
			
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