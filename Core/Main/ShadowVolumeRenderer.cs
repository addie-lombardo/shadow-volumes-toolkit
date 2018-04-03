// Shadow Volumes Toolkit
// Copyright 2013 Gustav Olsson
using UnityEngine;

[ExecuteInEditMode]
public class ShadowVolumeRenderer : MonoBehaviour
{
	public Material stencilClear, stencilInterpolate;
	public Material alphaClear, alphaClamp, alphaStrength, alphaInterpolate;
	public Material alphaFlip0, alphaFlip1, alphaFlip2, alphaFlip3;
	
	[SerializeField]
	protected ShadowVolumeBackend backend = ShadowVolumeBackend.AlphaChannel;
	
	[SerializeField]
	protected int layer;
	
	[SerializeField]
	protected Mesh quadMesh;
	
	protected static ShadowVolumeRenderer instance;
	
	public static void ResetInstance()
	{
		instance = null;
	}
	
	public static ShadowVolumeRenderer Instance
	{
		get
		{
			if (instance == null)
			{
				instance = (ShadowVolumeRenderer)GameObject.FindObjectOfType(typeof(ShadowVolumeRenderer));
			}
			
			return instance;
		}
	}
	
	/// <summary>
	/// Gets or sets the backend.
	/// </summary>
	public ShadowVolumeBackend Backend
	{
		get { return backend; }
		set { backend = value; }
	}
	
	/// <summary>
	/// Gets or sets the layer this shadow volume renderer is responsible for rendering.
	/// </summary>
	public int Layer
	{
		get { return layer; }
		set { layer = value; }
	}
	
	protected void CreateQuadMesh()
	{
		if (quadMesh == null)
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
			
			quadMesh = mesh;
		}
	}
	
	protected void DrawQuadMesh()
	{
		if (quadMesh == null)
		{
			return;
		}
		
		if (backend == ShadowVolumeBackend.StencilBuffer || backend == ShadowVolumeBackend.StencilBufferNoTwoSided)
		{
			Graphics.DrawMesh(quadMesh, Vector3.zero, Quaternion.identity, stencilClear, layer, null, 0, null, false, false);
			Graphics.DrawMesh(quadMesh, Vector3.zero, Quaternion.identity, stencilInterpolate, layer, null, 0, null, false, false);
		}
		else if (backend == ShadowVolumeBackend.AlphaChannel)
		{
			Graphics.DrawMesh(quadMesh, Vector3.zero, Quaternion.identity, alphaClear, layer, null, 0, null, false, false);
			Graphics.DrawMesh(quadMesh, Vector3.zero, Quaternion.identity, alphaClamp, layer, null, 0, null, false, false);
			Graphics.DrawMesh(quadMesh, Vector3.zero, Quaternion.identity, alphaStrength, layer, null, 0, null, false, false);
			Graphics.DrawMesh(quadMesh, Vector3.zero, Quaternion.identity, alphaInterpolate, layer, null, 0, null, false, false);
		}
		else if (backend == ShadowVolumeBackend.AlphaChannelNoBlendOp)
		{
			Graphics.DrawMesh(quadMesh, Vector3.zero, Quaternion.identity, alphaClear, layer, null, 0, null, false, false);
			Graphics.DrawMesh(quadMesh, Vector3.zero, Quaternion.identity, alphaFlip0, layer, null, 0, null, false, false);
			Graphics.DrawMesh(quadMesh, Vector3.zero, Quaternion.identity, alphaFlip1, layer, null, 0, null, false, false);
			Graphics.DrawMesh(quadMesh, Vector3.zero, Quaternion.identity, alphaFlip2, layer, null, 0, null, false, false);
			Graphics.DrawMesh(quadMesh, Vector3.zero, Quaternion.identity, alphaFlip3, layer, null, 0, null, false, false);
			Graphics.DrawMesh(quadMesh, Vector3.zero, Quaternion.identity, alphaClamp, layer, null, 0, null, false, false);
			Graphics.DrawMesh(quadMesh, Vector3.zero, Quaternion.identity, alphaStrength, layer, null, 0, null, false, false);
			Graphics.DrawMesh(quadMesh, Vector3.zero, Quaternion.identity, alphaInterpolate, layer, null, 0, null, false, false);
		}
	}
	
	public void Start()
	{
		ResetInstance();
		
		CreateQuadMesh();
	}
	
	public void Update()
	{
		DrawQuadMesh();
	}
	
	public void OnDestroy()
	{
		DestroyImmediate(quadMesh);
	}
}