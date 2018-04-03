// Shadow Volumes Toolkit
// Copyright 2012 Gustav Olsson
Shader "Shadow Volumes/Screen Flip 0"
{
	SubShader
	{
		Tags
		{
			"Queue" = "Overlay+502"
			"IgnoreProjector" = "True"
		}
		
		UsePass "Shadow Volumes/Screen Flip/FLIP"
	}
}