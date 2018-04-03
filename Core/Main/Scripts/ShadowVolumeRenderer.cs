// Shadow Volumes Toolkit
// Copyright 2012 Gustav Olsson
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class ShadowVolumeRenderer : MonoBehaviour
{
	public Material screenClear, screenFlip0, screenFlip1, screenFlip2, screenFlip3, screenClamp, screenStrength, screenInterpolate;
	
	[SerializeField]
	private ShadowCompatibilityMode mode;
	
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
	
	private void CreateMesh()
	{
		MeshFilter meshFilter = GetComponent<MeshFilter>();
		
		if (meshFilter.sharedMesh == null)
		{
			// Create quad vertices and triangles
			Vector3[] vertices =
			{
				new Vector3(-1.0f, 1.0f, 0.0f),
				new Vector3(1.0f, 1.0f, 0.0f),
				new Vector3(-1.0f, -1.0f, 0.0f),
				new Vector3(1.0f, -1.0f, 0.0f)
			};
			
			int[] triangles =
			{
				0, 1, 2,
				2, 1, 3
			};
			
			// Create the quad mesh
			Mesh mesh = new Mesh();
			
			mesh.name = "Quad Mesh";
			mesh.vertices = vertices;
			mesh.triangles = triangles;
			mesh.bounds = new Bounds(Vector3.zero, Vector3.one * float.MaxValue);
			
			meshFilter.sharedMesh = mesh;
		}
	}
	
	private void SetMaterials()
	{
		if (mode == ShadowCompatibilityMode.Standard)
		{
			GetComponent<Renderer>().sharedMaterials = new Material[] { screenClear, screenClamp, screenStrength, screenInterpolate };
		}
		else if (mode == ShadowCompatibilityMode.NoBlendOp)
		{
			GetComponent<Renderer>().sharedMaterials = new Material[] { screenClear, screenFlip0, screenFlip1, screenFlip2, screenFlip3, screenClamp, screenStrength, screenInterpolate };
		}
	}
	
	private bool HasMaterials()
	{
		Material[] materials = GetComponent<Renderer>().sharedMaterials;
		
		if (mode == ShadowCompatibilityMode.Standard)
		{
			if (materials.Length == 4 &&
				materials[0] == screenClear &&
				materials[1] == screenClamp &&
				materials[2] == screenStrength &&
				materials[3] == screenInterpolate)
			{
				return true;
			}
		}
		else if (mode == ShadowCompatibilityMode.NoBlendOp)
		{
			if (materials.Length == 8 &&
				materials[0] == screenClear &&
				materials[1] == screenFlip0 &&
				materials[2] == screenFlip1 &&
				materials[3] == screenFlip2 &&
				materials[4] == screenFlip3 &&
				materials[5] == screenClamp &&
				materials[6] == screenStrength &&
				materials[7] == screenInterpolate)
			{
				return true;
			}
		}
		
		return false;
	}
	
	public void Start()
	{
		CreateMesh();
		
		if (!HasMaterials())
		{
			SetMaterials();
		}
	}
	
	public void OnDestroy()
	{
		DestroyImmediate(GetComponent<MeshFilter>().sharedMesh);
	}
}