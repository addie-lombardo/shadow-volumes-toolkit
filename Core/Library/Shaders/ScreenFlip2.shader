// Shadow Volumes Toolkit
// Copyright 2012 Gustav Olsson
Shader "Shadow Volumes/Screen Flip 2"
{
	SubShader
	{
		Tags
		{
			"Queue" = "Overlay+506"
			"IgnoreProjector" = "True"
		}
		
		UsePass "Shadow Volumes/Screen Flip/FLIP"
	}
}