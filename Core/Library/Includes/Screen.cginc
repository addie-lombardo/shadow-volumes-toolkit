// Shadow Volumes Toolkit
// Copyright 2012 Gustav Olsson
float4 _shadowVolumeColor;

struct VertexInput
{
	float4 vertex : POSITION;
};

struct VertexOutput
{
	float4 position : POSITION;
};

VertexOutput ScreenVertex(VertexInput input)
{
	VertexOutput output;
	
	output.position = input.vertex;
	
	return output;
}

float4 ScreenFragment() : SV_Target
{
	return float4(1.0f, 1.0f, 1.0f, 1.0f);
}

float4 ScreenColorFragment() : SV_Target
{
	return _shadowVolumeColor;
}
