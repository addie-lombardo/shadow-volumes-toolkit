// Shadow Volumes Toolkit
// Copyright 2013 Gustav Olsson
Shader "Shadow Volumes/Alpha Flip 3"
{
	SubShader
	{
		Tags
		{
			"Queue" = "AlphaTest+508"
			"IgnoreProjector" = "True"
		}
		
		UsePass "Shadow Volumes/Alpha Flip/FLIP"
	}
}