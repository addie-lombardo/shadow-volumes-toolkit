// Shadow Volumes Toolkit
// Copyright 2013 Gustav Olsson
Shader "Shadow Volumes/Alpha Flip 2"
{
	SubShader
	{
		Tags
		{
			"Queue" = "AlphaTest+506"
			"IgnoreProjector" = "True"
		}
		
		UsePass "Shadow Volumes/Alpha Flip/FLIP"
	}
}