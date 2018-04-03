// Shadow Volumes Toolkit
// Copyright 2012 Gustav Olsson
Shader "Shadow Volumes/Screen Clamp"
{
	SubShader
	{
		Tags
		{
			"Queue" = "Overlay+509"
			"IgnoreProjector" = "True"
		}

		// Use the inherent clamping to convert shadowed areas to 1; non-shadowed areas will remain at 0
		UsePass "Shadow Volumes/Screen Double/DOUBLE"
		UsePass "Shadow Volumes/Screen Double/DOUBLE"
		UsePass "Shadow Volumes/Screen Double/DOUBLE"
		UsePass "Shadow Volumes/Screen Double/DOUBLE"
		UsePass "Shadow Volumes/Screen Double/DOUBLE"
		UsePass "Shadow Volumes/Screen Double/DOUBLE"
		UsePass "Shadow Volumes/Screen Double/DOUBLE"
		UsePass "Shadow Volumes/Screen Double/DOUBLE"
	}
}