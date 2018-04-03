// Shadow Volumes Toolkit
// Copyright 2013 Gustav Olsson
using UnityEngine;
using UnityEditor;

public class ShadowMeshCreatorWindow : EditorWindow
{
	protected static string helpText = "This script will create a shadow mesh asset given a source mesh asset. For best performance, source meshes should be two-manifold (closed). That is, each edge of the mesh should belong to exactly two triangles. The local bounds of each shadow mesh created will be extended by the bounds margin value in order to render the shadow even if the game object is outside the view frustum. If the shadows disappear when they should not, increase the bounds margin and recreate the mesh.";
	
	protected float boundsMargin = 2.0f;
	
	protected Mesh referenceMesh;
	
	[MenuItem("Window/Shadow Volumes Toolkit/Shadow Mesh Creator")]
	public static void ShowWindow()
	{
		EditorWindow window = EditorWindow.GetWindow<ShadowMeshCreatorWindow>();
		
		window.title = "Shadow Mesh Creator";
	}
	
	public void OnGUI()
	{
		// Show initial GUI components
		EditorGUILayout.HelpBox(helpText, MessageType.Info);
		
		boundsMargin = EditorGUILayout.FloatField("Bounds Margin", boundsMargin);
		
		referenceMesh = (Mesh)EditorGUILayout.ObjectField("Reference Mesh", referenceMesh, typeof(Mesh), false);
		
		if (referenceMesh != null)
		{
			string referencePath = AssetDatabase.GetAssetPath(referenceMesh);
			string shadowPath = ShadowAssetCreator.ConstructAssetPath(referenceMesh);
			
			// Show additional GUI components
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.PrefixLabel("Reference Mesh location:");
			EditorGUILayout.LabelField(referencePath);
			EditorGUILayout.EndHorizontal();
			
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.PrefixLabel("Shadow Mesh location:");
			EditorGUILayout.LabelField(shadowPath);
			EditorGUILayout.EndHorizontal();
			
			if (GUILayout.Button("Create shadow mesh"))
			{
				ShadowAssetCreator.CreateAsset(referenceMesh, boundsMargin, shadowPath);
			}
		}
	}
}