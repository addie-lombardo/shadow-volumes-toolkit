// Shadow Volumes Toolkit
// Copyright 2013 Gustav Olsson
Shader "Shadow Volumes/Alpha Flip"
{
	SubShader
	{
		Pass
		{
			Name "FLIP"
			
			Lighting Off
			Cull Off
			ZTest Always
			ZWrite Off
			Blend Zero One, OneMinusDstAlpha Zero
			
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