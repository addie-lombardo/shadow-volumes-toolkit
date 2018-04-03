// Shadow Volumes Toolkit
// Copyright 2012 Gustav Olsson
Shader "Shadow Volumes/Screen Flip 3"
{
	SubShader
	{
		Tags
		{
			"Queue" = "Overlay+508"
			"IgnoreProjector" = "True"
		}
		
		UsePass "Shadow Volumes/Screen Flip/FLIP"
	}
}