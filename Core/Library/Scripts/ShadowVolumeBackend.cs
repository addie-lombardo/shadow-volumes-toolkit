using UnityEngine;

public enum ShadowVolumeBackend
{
	StencilBuffer,
	StencilBufferNoTwoSided,
	AlphaChannel,
	AlphaChannelNoBlendOp
}