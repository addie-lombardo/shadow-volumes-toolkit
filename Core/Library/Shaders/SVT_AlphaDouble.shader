// Shadow Volumes Toolkit
// Copyright 2013 Gustav Olsson
Shader "Shadow Volumes/Alpha Double"
{
	SubShader
	{
		Pass
		{
			Name "DOUBLE"
			
			Lighting Off
			Cull Off
			ZTest Always
			ZWrite Off
			Blend Zero One, DstAlpha One
			
			Fog
			{
				Mode Off
			}
			
			CGPROGRAM
			#pragma vertex ScreenVertex
			#pragma fragment ScreenFragment
			#include "../Includes/Screen.cginc"
			ENDCG
		}
	}
}