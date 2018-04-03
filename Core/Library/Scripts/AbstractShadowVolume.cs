// Shadow Volumes Toolkit
// Copyright 2012 Gustav Olsson
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractShadowVolume : MonoBehaviour
{
	public Material backAlways, frontAlways, frontAlwaysNoBlendOp, front, back, backNoBlendOp;
	
	[SerializeField]
	private ShadowCompatibilityMode mode;
	
	[SerializeField]
	private bool isSimple = false;
	
	/// <summary>
	/// Gets or sets the compatibility mode. Use NoBlendOp for platforms that don't reliably support the BlendOp shader function, for example Android. The Standard mode uses less full screen passes and has better performance than the NoBlendOp mode. Always set the same compatibility mode for the ShadowVolumeRenderer, ShadowVolume and SkinnedShadowVolume scripts.
	/// </summary>
	public ShadowCompatibilityMode CompatibilityMode
	{
		get { return mode; }
		
		set
		{
			if (mode != value)
			{
				mode = value;
				
				SetMaterials();
			}
		}
	}
	
	/// <summary>
	/// Gets or sets whether this shadow volume is simple or not. Simple shadow volumes have better performance than non-simple shadow volumes but do not support the scenario where the camera is inside a volume.
	/// </summary>
	public bool IsSimple
	{
		get { return isSimple; }
		
		set
		{
			if (isSimple != value)
			{
				isSimple = value;
				
				SetMaterials();
			}
		}
	}
	
	private void SetMaterials()
	{
		if (mode == ShadowCompatibilityMode.Standard)
		{
			if (isSimple)
			{
				GetComponent<Renderer>().sharedMaterials = new Material[] { front, back };
			}
			else
			{
				GetComponent<Renderer>().sharedMaterials = new Material[] { backAlways, frontAlways, front, back };
			}
		}
		else if (mode == ShadowCompatibilityMode.NoBlendOp)
		{
			if (isSimple)
			{
				GetComponent<Renderer>().sharedMaterials = new Material[] { front, backNoBlendOp };
			}
			else
			{
				GetComponent<Renderer>().sharedMaterials = new Material[] { backAlways, frontAlwaysNoBlendOp, front, backNoBlendOp };
			}
		}
	}
	
	private bool HasMaterials()
	{
		Material[] materials = GetComponent<Renderer>().sharedMaterials;
		
		if (mode == ShadowCompatibilityMode.Standard)
		{
			if (isSimple)
			{
				if (materials.Length == 2 &&
					materials[0] == front &&
					materials[1] == back)
				{
					return true;
				}
			}
			else
			{
				if (materials.Length == 4 &&
					materials[0] == backAlways &&
					materials[1] == frontAlways &&
					materials[2] == front &&
					materials[3] == back)
				{
					return true;
				}
			}
		}
		else if (mode == ShadowCompatibilityMode.NoBlendOp)
		{
			if (isSimple)
			{
				if (materials.Length == 2 &&
					materials[0] == front &&
					materials[1] == backNoBlendOp)
				{
					return true;
				}
			}
			else
			{
				if (materials.Length == 4 &&
					materials[0] == backAlways &&
					materials[1] == frontAlwaysNoBlendOp &&
					materials[2] == front &&
					materials[3] == backNoBlendOp)
				{
					return true;
				}
			}
		}
		
		return false;
	}
	
	public virtual void Start()
	{
		if (!HasMaterials())
		{
			SetMaterials();
		}
	}
}