// Shadow Volumes Toolkit
// Copyright 2013 Gustav Olsson
using UnityEngine;

[ExecuteInEditMode]
public class ShadowVolume : MonoBehaviour
{
	public Material stencilBackFrontAlways, stencilFrontBack;
	public Material stencilBackAlways, stencilFrontAlways, stencilFront, stencilBack;
	public Material alphaBackAlways, alphaFrontAlways, alphaFront, alphaBack;
	public Material alphaFrontAlwaysNoBlendOp, alphaBackNoBlendOp;
	
	[SerializeField]
	protected Mesh shadowMesh;
	
	[SerializeField]
	protected bool isSimple;
	
	[SerializeField]
	protected int layer;
	
	/// <summary>
	/// Gets or sets the shadow mesh. The shadow mesh is a special mesh that is used for shadow volume extrusion.
	/// </summary>
	public Mesh ShadowMesh
	{
		get { return shadowMesh; }
		set { shadowMesh = value; }
	}
	
	/// <summary>
	/// Gets or sets whether this shadow volume is simple or not. Simple shadow volumes are less resource intensive than non-simple shadow volumes but do not work when the camera is located inside a volume.
	/// </summary>
	public bool IsSimple
	{
		get { return isSimple; }
		set { isSimple = value; }
	}
	
	/// <summary>
	/// Gets or sets the layer to which this shadow volume is rendered.
	/// </summary>
	public int Layer
	{
		get { return layer; }
		set { layer = value; }
	}

	protected void DrawShadowMesh()
	{
		if (shadowMesh == null)
		{
			return;
		}
		
		ShadowVolumeRenderer shadowVolumeRenderer = ShadowVolumeRenderer.Instance;
		
		if (shadowVolumeRenderer == null)
		{
			return;
		}
		
		ShadowVolumeBackend backend = shadowVolumeRenderer.Backend;

        Matrix4x4 shadowTransform = transform.localToWorldMatrix;
		
		if (backend == ShadowVolumeBackend.StencilBuffer)
		{
			if (isSimple)
			{
				Graphics.DrawMesh(shadowMesh, shadowTransform, stencilFrontBack, layer, null, 0, null, false, false);
			}
			else
			{
				Graphics.DrawMesh(shadowMesh, shadowTransform, stencilBackFrontAlways, layer, null, 0, null, false, false);
				Graphics.DrawMesh(shadowMesh, shadowTransform, stencilFrontBack, layer, null, 0, null, false, false);
			}
		}
		else if (backend == ShadowVolumeBackend.StencilBufferNoTwoSided)
		{
			if (isSimple)
			{
				Graphics.DrawMesh(shadowMesh, shadowTransform, stencilFront, layer, null, 0, null, false, false);
				Graphics.DrawMesh(shadowMesh, shadowTransform, stencilBack, layer, null, 0, null, false, false);
			}
			else
			{
				Graphics.DrawMesh(shadowMesh, shadowTransform, stencilBackAlways, layer, null, 0, null, false, false);
				Graphics.DrawMesh(shadowMesh, shadowTransform, stencilFrontAlways, layer, null, 0, null, false, false);
				Graphics.DrawMesh(shadowMesh, shadowTransform, stencilFront, layer, null, 0, null, false, false);
				Graphics.DrawMesh(shadowMesh, shadowTransform, stencilBack, layer, null, 0, null, false, false);
			}
		}
		else if (backend == ShadowVolumeBackend.AlphaChannel)
		{
			if (isSimple)
			{
				Graphics.DrawMesh(shadowMesh, shadowTransform, alphaFront, layer, null, 0, null, false, false);
				Graphics.DrawMesh(shadowMesh, shadowTransform, alphaBack, layer, null, 0, null, false, false);
			}
			else
			{
				Graphics.DrawMesh(shadowMesh, shadowTransform, alphaBackAlways, layer, null, 0, null, false, false);
				Graphics.DrawMesh(shadowMesh, shadowTransform, alphaFrontAlways, layer, null, 0, null, false, false);
				Graphics.DrawMesh(shadowMesh, shadowTransform, alphaFront, layer, null, 0, null, false, false);
				Graphics.DrawMesh(shadowMesh, shadowTransform, alphaBack, layer, null, 0, null, false, false);
			}
		}
		else if (backend == ShadowVolumeBackend.AlphaChannelNoBlendOp)
		{
			if (isSimple)
			{
				Graphics.DrawMesh(shadowMesh, shadowTransform, alphaFront, layer, null, 0, null, false, false);
				Graphics.DrawMesh(shadowMesh, shadowTransform, alphaBackNoBlendOp, layer, null, 0, null, false, false);
			}
			else
			{
				Graphics.DrawMesh(shadowMesh, shadowTransform, alphaBackAlways, layer, null, 0, null, false, false);
				Graphics.DrawMesh(shadowMesh, shadowTransform, alphaFrontAlwaysNoBlendOp, layer, null, 0, null, false, false);
				Graphics.DrawMesh(shadowMesh, shadowTransform, alphaFront, layer, null, 0, null, false, false);
				Graphics.DrawMesh(shadowMesh, shadowTransform, alphaBackNoBlendOp, layer, null, 0, null, false, false);
			}
		}
	}
	
	public void Update()
	{
		DrawShadowMesh();
	}
}