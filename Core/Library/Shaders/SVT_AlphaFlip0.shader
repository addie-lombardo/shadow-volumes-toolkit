// Shadow Volumes Toolkit
// Copyright 2013 Gustav Olsson
Shader "Shadow Volumes/Alpha Flip 0"
{
	SubShader
	{
		Tags
		{
			"Queue" = "AlphaTest+502"
			"IgnoreProjector" = "True"
		}
		
		UsePass "Shadow Volumes/Alpha Flip/FLIP"
	}
}