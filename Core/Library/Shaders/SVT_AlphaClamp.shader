// Shadow Volumes Toolkit
// Copyright 2013 Gustav Olsson
Shader "Shadow Volumes/Alpha Clamp"
{
	SubShader
	{
		Tags
		{
			"Queue" = "AlphaTest+509"
			"IgnoreProjector" = "True"
		}

		// Use the inherent clamping to convert shadowed areas to 1; non-shadowed areas will remain at 0
		UsePass "Shadow Volumes/Alpha Double/DOUBLE"
		UsePass "Shadow Volumes/Alpha Double/DOUBLE"
		UsePass "Shadow Volumes/Alpha Double/DOUBLE"
		UsePass "Shadow Volumes/Alpha Double/DOUBLE"
		UsePass "Shadow Volumes/Alpha Double/DOUBLE"
		UsePass "Shadow Volumes/Alpha Double/DOUBLE"
		UsePass "Shadow Volumes/Alpha Double/DOUBLE"
		UsePass "Shadow Volumes/Alpha Double/DOUBLE"
	}
}