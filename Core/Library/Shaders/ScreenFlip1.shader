// Shadow Volumes Toolkit
// Copyright 2012 Gustav Olsson
Shader "Shadow Volumes/Screen Flip 1"
{
	SubShader
	{
		Tags
		{
			"Queue" = "Overlay+504"
			"IgnoreProjector" = "True"
		}
		
		UsePass "Shadow Volumes/Screen Flip/FLIP"
	}
}