// Shadow Volumes Toolkit
// Copyright 2012 Gustav Olsson
float4 _shadowVolumeColor;

struct VertexInput
{
	float4 vertex : SV_POSITION;
};

struct VertexOutput
{
	float4 position : SV_POSITION;
};

VertexOutput ScreenVertex(VertexInput input)
{
	VertexOutput output;
	
	output.position = input.vertex;
	
	return output;
}

float4 ScreenFragment() : COLOR
{
	return float4(1.0f, 1.0f, 1.0f, 1.0f);
}

float4 ScreenColorFragment() : COLOR
{
	return _shadowVolumeColor;
}