// Shadow Volumes Toolkit
// Copyright 2013 Gustav Olsson
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(SkinnedMeshRenderer))]
public class SkinnedShadowVolume : MonoBehaviour
{
	public Material stencilBackFrontAlways, stencilFrontBack;
	public Material stencilBackAlways, stencilFrontAlways, stencilFront, stencilBack;
	public Material alphaBackAlways, alphaFrontAlways, alphaFront, alphaBack;
	public Material alphaFrontAlwaysNoBlendOp, alphaBackNoBlendOp;
	
	[SerializeField]
	protected bool isSimple;
	
	[SerializeField]
	protected ShadowVolumeBackend backend;
	
	protected bool updateMaterials;
	
	/// <summary>
	/// Gets or sets whether this shadow volume is simple or not. Simple shadow volumes are less resource intensive than non-simple shadow volumes but do not work when the camera is located inside a volume.
	/// </summary>
	public bool IsSimple
	{
		get { return isSimple; }
		
		set
		{
			if (isSimple != value)
			{
				isSimple = value;
				
				updateMaterials = true;
			}
		}
	}
	
	protected bool IsSetupCorrectly()
	{
		Material[] materials = GetComponent<Renderer>().sharedMaterials;
		
		foreach (Material material in materials)
		{
			if (material == null)
			{
				return false;
			}
		}
		
		return true;
	}
	
	protected void SetShadowMaterials()
	{
		ShadowVolumeRenderer shadowVolumeRenderer = ShadowVolumeRenderer.Instance;
		
		if (shadowVolumeRenderer == null)
		{
			return;
		}
		
		ShadowVolumeBackend rendererBackend = shadowVolumeRenderer.Backend;
		
		if (backend == rendererBackend && updateMaterials == false)
		{
			return;
		}
		
		backend = rendererBackend;
		updateMaterials = false;
		
		if (backend == ShadowVolumeBackend.StencilBuffer)
		{
			if (isSimple)
			{
				GetComponent<Renderer>().sharedMaterials = new Material[] { stencilFrontBack };
			}
			else
			{
				GetComponent<Renderer>().sharedMaterials = new Material[] { stencilBackFrontAlways, stencilFrontBack };
			}
		}
		else if (backend == ShadowVolumeBackend.StencilBufferNoTwoSided)
		{
			if (isSimple)
			{
				GetComponent<Renderer>().sharedMaterials = new Material[] { stencilFront, stencilBack };
			}
			else
			{
				GetComponent<Renderer>().sharedMaterials = new Material[] { stencilBackAlways, stencilFrontAlways, stencilFront, stencilBack };
			}
		}
		else if (backend == ShadowVolumeBackend.AlphaChannel)
		{
			if (isSimple)
			{
				GetComponent<Renderer>().sharedMaterials = new Material[] { alphaFront, alphaBack };
			}
			else
			{
				GetComponent<Renderer>().sharedMaterials = new Material[] { alphaBackAlways, alphaFrontAlways, alphaFront, alphaBack };
			}
		}
		else if (backend == ShadowVolumeBackend.AlphaChannelNoBlendOp)
		{
			if (isSimple)
			{
				GetComponent<Renderer>().sharedMaterials = new Material[] { alphaFront, alphaBackNoBlendOp };
			}
			else
			{
				GetComponent<Renderer>().sharedMaterials = new Material[] { alphaBackAlways, alphaFrontAlwaysNoBlendOp, alphaFront, alphaBackNoBlendOp };
			}
		}
	}
	
	public void Start()
	{
		if (!IsSetupCorrectly())
		{
			updateMaterials = true;
		}
	}
	
	public void Update()
	{
		SetShadowMaterials();
	}
}