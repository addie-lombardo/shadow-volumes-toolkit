// Shadow Volumes Toolkit
// Copyright 2013 Gustav Olsson
Shader "Shadow Volumes/Alpha Flip 1"
{
	SubShader
	{
		Tags
		{
			"Queue" = "AlphaTest+504"
			"IgnoreProjector" = "True"
		}
		
		UsePass "Shadow Volumes/Alpha Flip/FLIP"
	}
}