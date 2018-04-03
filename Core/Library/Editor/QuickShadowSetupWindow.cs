// Shadow Volumes Toolkit
// Copyright 2013 Gustav Olsson
using UnityEngine;
using UnityEditor;

public class QuickShadowSetupWindow : EditorWindow
{
	protected static string helpText = "This script will setup shadows for each renderable mesh in the selected game object hierarchy. For best performance, source meshes should be two-manifold (closed). That is, each edge of the mesh should belong to exactly two triangles. The local bounds of each shadow mesh created will be extended by the bounds margin value in order to render the shadow even if the game object is outside the view frustum. If the shadows disappear when they should not, increase the bounds margin and recreate the mesh.";
	
	protected bool setupChildren = true;
	protected bool createShadowMeshes = true;
	protected float boundsMargin = 2.0f;
	protected bool isSimple = false;
	protected int layer = 0;
	
	protected void SetupGameObject(Transform transform)
	{
		// Destroy existing shadow game objects before performing recursion
		foreach (Transform child in transform)
		{
			if (child.name.Contains("Shadow"))
			{
				DestroyImmediate(child.gameObject);
			}
		}
		
		// Recursively setup (non-shadow game object) children first
		if (setupChildren)
		{
			foreach (Transform child in transform)
			{
				SetupGameObject(child);
			}
		}
		
		// Add shadow game object
		if (!transform.name.Contains("Shadow"))
		{
			// Examine current game object
			MeshFilter meshFilter = transform.GetComponent<MeshFilter>();
			SkinnedMeshRenderer skinnedMeshRenderer = transform.GetComponent<SkinnedMeshRenderer>();
			
			if (meshFilter != null && meshFilter.sharedMesh != null)
			{
				// Add shadow volume component
				ShadowVolume shadowVolume = transform.gameObject.GetComponent<ShadowVolume>();
				
				if (shadowVolume == null)
				{
					shadowVolume = transform.gameObject.AddComponent<ShadowVolume>();
				}
				
				shadowVolume.IsSimple = isSimple;
				shadowVolume.Layer = layer;
				
				if (createShadowMeshes)
				{
					Mesh shadowMesh = ShadowAssetCreator.CreateAsset(meshFilter.sharedMesh, boundsMargin);
					
					shadowVolume.ShadowMesh = shadowMesh;
				}
			}
			else if (skinnedMeshRenderer != null && skinnedMeshRenderer.sharedMesh != null)
			{
				// Create skinned shadow game object
				GameObject shadowGameObject = new GameObject("Skinned Shadow");
				
				// Set Transform
				shadowGameObject.transform.parent = transform;
				shadowGameObject.transform.localPosition = Vector3.zero;
				shadowGameObject.transform.localRotation = Quaternion.identity;
				shadowGameObject.transform.localScale = Vector3.one;
				
				// Set SkinnedMeshRenderer
				SkinnedMeshRenderer shadowRenderer = shadowGameObject.AddComponent<SkinnedMeshRenderer>();
				
				if (createShadowMeshes)
				{
					Mesh shadowMesh = ShadowAssetCreator.CreateAsset(skinnedMeshRenderer.sharedMesh, boundsMargin);
					
					if (shadowMesh != null)
					{
						shadowRenderer.bones = skinnedMeshRenderer.bones;
						shadowRenderer.sharedMesh = shadowMesh;
					}
				}
				
				// Set SkinnedShadowVolume
				SkinnedShadowVolume shadowVolume = shadowGameObject.AddComponent<SkinnedShadowVolume>();
				
				shadowVolume.IsSimple = isSimple;
				shadowVolume.gameObject.layer = layer;
			}
		}
	}
	
	[MenuItem("Window/Shadow Volumes Toolkit/Quick Shadow Setup")]
	public static void ShowWindow()
	{
		EditorWindow window = EditorWindow.GetWindow<QuickShadowSetupWindow>();
		
		window.title = "Quick Shadow Setup";
	}
	
	public void OnSelectionChange()
	{
		EditorWindow window = EditorWindow.GetWindow<QuickShadowSetupWindow>();
		
		window.Repaint();
	}
	
	public void OnGUI()
	{
		GameObject gameObject = Selection.activeGameObject;
		
		// Show initial GUI components
		EditorGUILayout.HelpBox(helpText, MessageType.Info);
		
		setupChildren = EditorGUILayout.Toggle("Setup children", setupChildren);
		createShadowMeshes = EditorGUILayout.Toggle("Create Shadow Meshes", createShadowMeshes);
		
		boundsMargin = EditorGUILayout.FloatField("Bounds Margin", boundsMargin);
		
		isSimple = EditorGUILayout.Toggle("Is Simple", isSimple);
		
		layer = EditorGUILayout.LayerField("Layer", layer);
		
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel("Selected game object:");
		EditorGUILayout.LabelField(gameObject != null ? gameObject.name : "None");
		EditorGUILayout.EndHorizontal();
		
		if (gameObject != null)
		{
			// Show additional GUI components
			if (GUILayout.Button("Setup shadow"))
			{
				SetupGameObject(gameObject.transform);
			}
		}
	}
}