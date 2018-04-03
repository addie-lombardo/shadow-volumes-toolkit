// Shadow Volumes Toolkit
// Copyright 2012 Gustav Olsson
using UnityEngine;
using UnityEditor;

public class QuickShadowSetupEditor : EditorWindow
{
	private static string helpText = "Please select a game object in the scene to continue. The script will generate a shadow game object for each renderable mesh in the selected game object hierarchy. Each mesh needs to be closed (each edge belongs to exactly two triangles) to avoid shadow artifacts. The local bounds of each shadow mesh will be extended by the bounds margin value in order to render the shadow even when the game object is outside the view frustum. If the shadows disappear when they should not, increase the bounds margin and recreate the mesh.";
	
	private bool setupChildren = true;
	private bool createShadowMeshes = true;
	
	private float boundsMargin = 2.0f;
	
	private bool isSimple = false;
	private ShadowCompatibilityMode compatibilityMode = ShadowCompatibilityMode.Standard;
	
	private void SetupGameObject(Transform transform)
	{
		// Destroy existing shadow game objects in order to not create duplicates
		foreach (Transform child in transform)
		{
			if (child.name.Contains("Shadow"))
			{
				DestroyImmediate(child.gameObject);
			}
		}
		
		// Recursively setup children first
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
			
			GameObject shadow = null;
			
			if (meshFilter != null)
			{
				// Create shadow game object
				shadow = new GameObject("Shadow");
				shadow.AddComponent<ShadowVolume>();
				
				if (createShadowMeshes && meshFilter.sharedMesh != null)
				{
					Mesh shadowMesh = ShadowAssetCreator.CreateAsset(meshFilter.sharedMesh, boundsMargin);
					
					shadow.GetComponent<MeshFilter>().sharedMesh = shadowMesh;
				}
			}
			else if (skinnedMeshRenderer != null)
			{
				// Create skinned shadow game object
				shadow = new GameObject("Skinned Shadow");
				shadow.AddComponent<SkinnedShadowVolume>();
				
				if (createShadowMeshes && skinnedMeshRenderer.sharedMesh != null)
				{
					Mesh shadowMesh = ShadowAssetCreator.CreateAsset(skinnedMeshRenderer.sharedMesh, boundsMargin);
					
					if (shadowMesh != null)
					{
						SkinnedMeshRenderer shadowRenderer = shadow.GetComponent<SkinnedMeshRenderer>();
						
						shadowRenderer.bones = skinnedMeshRenderer.bones;
						shadowRenderer.sharedMesh = shadowMesh;
					}
				}
			}
			
			if (shadow != null)
			{
				// Set parent
				shadow.transform.parent = transform;
				
				// Reset transform
				shadow.transform.localPosition = Vector3.zero;
				shadow.transform.localRotation = Quaternion.identity;
				shadow.transform.localScale = Vector3.one;
				
				// Set shadow volume properties
				AbstractShadowVolume shadowVolume = shadow.GetComponent<AbstractShadowVolume>();
				
				shadowVolume.IsSimple = isSimple;
				shadowVolume.CompatibilityMode = compatibilityMode;
			}
		}
	}
	
	[MenuItem("Window/Shadow Volumes Toolkit/Quick Shadow Setup")]
	public static void ShowWindow()
	{
		EditorWindow window = EditorWindow.GetWindow<QuickShadowSetupEditor>();
		
		window.title = "Quick Shadow Setup";
	}
	
	public void OnSelectionChange()
	{
		EditorWindow window = EditorWindow.GetWindow<QuickShadowSetupEditor>();
		
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
		compatibilityMode = (ShadowCompatibilityMode)EditorGUILayout.EnumPopup("Compatibility Mode", compatibilityMode);
		
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